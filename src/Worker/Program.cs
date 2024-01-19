using OmniePDV.ReceiptSender.Core.Consumer;
using OmniePDV.ReceiptSender.Core.Contracts.Consumer;
using OmniePDV.ReceiptSender.Core.Contracts.Services;
using OmniePDV.ReceiptSender.Core.Models.Options;
using OmniePDV.ReceiptSender.Core.Services;
using OmniePDV.ReceiptSender.Worker;

var builder = Host.CreateApplicationBuilder(args);

builder.Services
    .Configure<ApiOptions>(
        builder.Configuration.GetSection(ApiOptions.Position))
    .Configure<RabbitMQOptions>(
        builder.Configuration.GetSection(RabbitMQOptions.Position))
    .Configure<MailSettingsOptions>(
        builder.Configuration.GetSection(MailSettingsOptions.Position));

builder.Services.AddTransient<IMessageConsumer, MessageConsumer>();
builder.Services.AddTransient<ISaleReceiptsService, SaleReceiptsService>();
builder.Services.AddTransient<IEmailSenderService, EmailSenderService>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
