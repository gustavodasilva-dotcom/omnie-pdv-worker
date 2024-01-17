using System.Text;
using Microsoft.Extensions.Options;
using OmniePDV.ReceiptSender.Worker.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OmniePDV.ReceiptSender.Worker;

public class Worker(
    ILogger<Worker> logger,
    IOptions<RabbitMQOptions> options) : BackgroundService
{
    private readonly ILogger<Worker> _logger = logger;
    private readonly RabbitMQOptions _options = options.Value;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            _logger.LogInformation("{time} - Worker running", DateTimeOffset.Now);

            ConnectionFactory factory = new()
            {
                HostName = _options.Authentication.HostName,
                UserName = _options.Authentication.UserName,
                Password = _options.Authentication.Password,
                VirtualHost = _options.Authentication.VirtualHost
            };

            IConnection connection = factory.CreateConnection();

            using IModel channel = connection.CreateModel();
            channel.QueueDeclare(_options.Queue, durable: true, exclusive: false);

            EventingBasicConsumer consumer = new(channel);
            consumer.Received += (model, eventArgs) =>
            {
                byte[] body = eventArgs.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);
                _logger.LogInformation(message);
            };
            channel.BasicConsume(
                queue: _options.Queue,
                autoAck: true,
                consumer: consumer
            );

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{time} - Error logged at {method}: {message}",
                DateTimeOffset.Now, nameof(ExecuteAsync), e.Message);
        }
    }
}
