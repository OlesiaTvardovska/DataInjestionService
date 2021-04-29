using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebScrapper.Core.Interfaces;

namespace WebScrapper.WorkerService
{
    public class ScrappingWorker: IHostedService, IDisposable
    {
        private int _executionCount;
        private readonly ILogger<ScrappingWorker> _logger;
        private Timer _timer;
        private IScrapperService _scrapper;

        public ScrappingWorker(ILogger<ScrappingWorker> logger, IScrapperService scrapper)
        {
            _logger = logger;
            _scrapper = scrapper;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Scrapping Service running...");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }
        private void DoWork(object state)
        {
            var count = Interlocked.Increment(ref _executionCount);

            _logger.LogInformation(
                "Scrapping Service running... Count: {Count}", count);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Scrapping Service stopping....");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
