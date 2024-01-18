using OmniePDV.ReceiptSender.Core.Contracts.Services;
using System.Threading;

namespace OmniePDV.ReceiptSender.Worker;

public class Worker(
    ILogger<Worker> logger,
    ISaleReceiptsService saleReceiptsService) : BackgroundService
{
    private readonly ILogger<Worker> _logger = logger;
    private readonly ISaleReceiptsService _saleReceiptsService = saleReceiptsService;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation($"{DateTimeOffset.Now} - Worker is running");
        try
        {
            _saleReceiptsService.HandleConsumer();
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Worker failed");
        }
    }
}
