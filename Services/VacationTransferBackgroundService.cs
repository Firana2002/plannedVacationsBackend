using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace VacationPlanner.Api.Services
{
    public class VacationTransferBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<VacationTransferBackgroundService> _logger;

        public VacationTransferBackgroundService(
            IServiceScopeFactory scopeFactory,
            ILogger<VacationTransferBackgroundService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
{
    while (!stoppingToken.IsCancellationRequested)
    {
        var now = DateTime.UtcNow;
        if (now.Month == 1 && now.Day == 1)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<VacationTransferService>();
                try
                {
                    await service.TransferVacations();
                    _logger.LogInformation("Vacations transferred successfully.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Transfer error.");
                }
            }
        }

        // Проверяем ежедневно
        await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
    }
}}}