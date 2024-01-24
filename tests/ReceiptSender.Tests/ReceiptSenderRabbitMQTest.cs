using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using OmniePDV.ReceiptSender.Core.Consumer;
using OmniePDV.ReceiptSender.Core.Models.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OmniePDV.ReceiptSender.Tests;

public class ReceiptSenderRabbitMQTest
{
    [Fact]
    public void Create_RabbitMQ_Connection_Channel_And_Queue()
    {
        RabbitMQOptions rabbitMQOptions = new()
        {
            Queue = "receipt-sender",
            Authentication = new()
            {
                HostName = "localhost",
                UserName = "user",
                Password = "mypass",
                VirtualHost = "/"
            }
        };

        Mock<IOptions<RabbitMQOptions>> rabbitMQOptionsMock = new();
        rabbitMQOptionsMock.Setup(s => s.Value).Returns(rabbitMQOptions);

        MessageConsumer messageConsumer = new(rabbitMQOptionsMock.Object);
        
        IModel channel = messageConsumer.CreateModel();
        channel.QueueDeclare(
            queue: rabbitMQOptions.Queue,
            durable: true,
            exclusive: false
        );

        EventingBasicConsumer consumer = new(channel);
        channel.BasicConsume(
            queue: rabbitMQOptions.Queue,
            autoAck: true,
            consumer: consumer
        );

        channel.IsOpen.Should().Be(true);
    }
}