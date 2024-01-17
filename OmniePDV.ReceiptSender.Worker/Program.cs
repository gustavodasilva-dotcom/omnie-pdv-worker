using OmniePDV.ReceiptSender.Worker;
using OmniePDV.ReceiptSender.Worker.Data;
using OmniePDV.ReceiptSender.Worker.Options;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services
    .Configure<MongoDBOptions>(
        builder.Configuration.GetSection(MongoDBOptions.Position))
    .Configure<RabbitMQOptions>(
        builder.Configuration.GetSection(RabbitMQOptions.Position));

builder.Services.AddScoped<IMongoContext, MongoContext>();

var host = builder.Build();
host.Run();
