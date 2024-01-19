using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OmniePDV.ReceiptSender.Core.Contracts.Consumer;
using OmniePDV.ReceiptSender.Core.Contracts.Services;
using OmniePDV.ReceiptSender.Core.Models.Options;
using OmniePDV.ReceiptSender.Core.Models.Payloads;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using OmniePDV.ReceiptSender.Core.Models.Resources;
using OmniePDV.ReceiptSender.Core.Resources.HtmlTemplates;

namespace OmniePDV.ReceiptSender.Core.Services;

public sealed class SaleReceiptsService(
    IOptions<RabbitMQOptions> rabbitOptions,
    IOptions<MailSettingsOptions> mailOptions,
    ILogger<SaleReceiptsService> logger,
    IMessageConsumer messageConsumer,
    IEmailSenderService emailSenderService) : ISaleReceiptsService
{
    private readonly RabbitMQOptions _rabbitOptions = rabbitOptions.Value;
    private readonly MailSettingsOptions _mailOptions = mailOptions.Value;
    private readonly ILogger<SaleReceiptsService> _logger = logger;
    private readonly IMessageConsumer _messageConsumer = messageConsumer;
    private readonly IEmailSenderService _emailSenderService = emailSenderService;

    private async Task SendSaleReceipt(SendReceiptEmailPlayload payload)
    {
        string receiptTemplate = ReceiptEmailTemplate.ContentTemplate;
        string itemsTemplate = string.Empty;

        foreach (var product in payload.Sale.Products)
        {
            string template = ReceiptEmailTemplate.TableRowTemplate;

            template = template
                .Replace("[item-description]", product.Product.Name)
                .Replace("[item-unitary-price]", string.Format("{0:C}",
                    product.Product.RetailPrice))
                .Replace("[item-quantity]", product.Quantity.ToString())
                .Replace("[item-total-price]", string.Format("{0:C}",
                    product.Product.RetailPrice * product.Quantity));
            
            itemsTemplate += template;
        }

        receiptTemplate = receiptTemplate.Replace("[[table-rows-content]]", itemsTemplate);
        
        string bodyTemplate = DefaultEmailTemplate.HtmlText;
        bodyTemplate = bodyTemplate.Replace("[[custom-content]]", receiptTemplate);

        await _emailSenderService.SendEmailAsync(new MailModel
        {
            Froms =
            [
                new(_mailOptions.NoReplySender.Name, _mailOptions.NoReplySender.Email)
            ],
            Tos =
            [
                new(payload.Sale.Client.Name, payload.Email)
            ],
            Subject = $"Sale {payload.Sale.Number} receipt",
            Body =
            {
                Parameters =
                {
                    { "[order-number]", payload.Sale.Number.ToString().PadLeft(4, '0') },
                    { "[order-client]", payload.Sale.Client.Name },
                    { "[total-amount]", string.Format("{0:C}", payload.Sale.Total) }
                },
                HtmlBody = bodyTemplate
            }
        });
    }

    public void HandleConsumer()
    {
        try
        {
            IModel channel = _messageConsumer.CreateModel();
            channel.QueueDeclare(
                queue: _rabbitOptions.Queue,
                durable: true,
                exclusive: false
            );

            EventingBasicConsumer consumer = new(channel);
            consumer.Received += async (model, eventArgs) =>
            {
                byte[] body = eventArgs.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);

                SendReceiptEmailPlayload response = JsonSerializer
                    .Deserialize<SendReceiptEmailPlayload>(message) ??
                    throw new Exception("Invalid message");

                await SendSaleReceipt(response);
            };
            channel.BasicConsume(
                queue: _rabbitOptions.Queue,
                autoAck: true,
                consumer: consumer
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error consuming queue");
        }
    }
}
