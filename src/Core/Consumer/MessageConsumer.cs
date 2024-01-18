using Microsoft.Extensions.Options;
using OmniePDV.ReceiptSender.Core.Contracts.Consumer;
using OmniePDV.ReceiptSender.Core.Models.Options;
using RabbitMQ.Client;

namespace OmniePDV.ReceiptSender.Core.Consumer;

public sealed class MessageConsumer(IOptions<RabbitMQOptions> options)
    : IMessageConsumer
{
    private readonly RabbitMQOptions _options = options.Value;
    private readonly IModel? _channel;

    private IConnection CreateConnection()
    {
        ConnectionFactory factory = new()
        {
            HostName = _options.Authentication.HostName,
            UserName = _options.Authentication.UserName,
            Password = _options.Authentication.Password,
            VirtualHost = _options.Authentication.VirtualHost
        };
        return factory.CreateConnection();
    }

    public IModel CreateModel()
    {
        IConnection connection = CreateConnection();
        return connection.CreateModel();
    }
}
