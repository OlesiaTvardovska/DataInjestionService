using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebScrapper.Core.Constants;
using WebScrapper.Core.Interfaces;
using WebScrapper.Sender;

namespace WebScrapper.WorkerService
{
    public class ScrappingWorker: IHostedService, IDisposable
    {
        private int _executionCount;
        private readonly ILogger<ScrappingWorker> _logger;
        private Timer _timer;
        private IScrapperService _scrapper;
        private RabbitMQSender _rabbitMQSender;

        public ScrappingWorker(ILogger<ScrappingWorker> logger, IScrapperService scrapper)
        {
            _logger = logger;
            _scrapper = scrapper;
            _rabbitMQSender = new RabbitMQSender();
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Scrapping Service running...");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(120));

            return Task.CompletedTask;
        }
        private void DoWork(object state)
        {
            var count = Interlocked.Increment(ref _executionCount);
            _scrapper.Init(Constants.googleNewsUrl);
            var items = _scrapper.DoScrapping();
            _rabbitMQSender.SendData(items);
            _scrapper.CloseBrowser();
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
