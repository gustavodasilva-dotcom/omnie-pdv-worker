using RabbitMQ.Client;

namespace OmniePDV.ReceiptSender.Core.Contracts.Consumer;

public interface IMessageConsumer
{
    IModel CreateModel();
}
