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
    public class ScrappingWorker: BackgroundService
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

            return Task.CompletedTask;
        }
        private void DoWork(object state)
        {
            var count = Interlocked.Increment(ref _executionCount);
            _scrapper.Init(Constants.googleNewsUrl);
            var items = _scrapper.DoScrapping();
            _rabbitMQSender.SendData(items);
            _logger.LogInformation(
                "Scrapping Service running... Count: {Count}", count);
        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Scrapping Service stopping....");

            _timer?.Change(Timeout.Infinite, 0);
            _scrapper.CloseBrowser();
            _scrapper.Dispose();
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _timer?.Dispose();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Scrapping Service running...");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(100));

            return Task.CompletedTask;
        }
    }
}
