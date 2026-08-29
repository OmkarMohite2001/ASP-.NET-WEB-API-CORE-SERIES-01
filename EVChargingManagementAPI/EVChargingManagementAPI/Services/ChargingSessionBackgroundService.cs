using EVChargingManagementAPI.Data;
using Microsoft.EntityFrameworkCore;
namespace EVChargingManagementAPI.Services
{
    public class ChargingSessionBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<ChargingSessionBackgroundService> _logger;

        public ChargingSessionBackgroundService(
            IServiceScopeFactory scopeFactory,
            ILogger<ChargingSessionBackgroundService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();

                    var context = scope.ServiceProvider
                        .GetRequiredService<AppDbContext>();

                    var sessions = await context.ChargingSessions
                        .Where(s => s.Status == "Completed")
                        .ToListAsync(stoppingToken);

                    _logger.LogInformation(
                        "Completed charging sessions found: {Count}",
                        sessions.Count);
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Error occurred in background service.");
                }

                await Task.Delay(
                    TimeSpan.FromSeconds(30),
                    stoppingToken);
            }
        }
    }
}
