using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebScrapper.Application.Features.NewsFeatures.Commands;
using WebScrapper.Core.Interfaces;
using WebScrapper.Core.Models;

namespace WebScrapper.ConsumerService
{
    public class Worker : IHostedService, IDisposable
    {
        private int _executionCount;
        private readonly ILogger<Worker> _logger;
        private Timer _timer;
        private IMediator _mediator;
        private IServiceScopeFactory _serviceScopeFactory;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Consumer Service running...");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromMinutes(1));

            return Task.CompletedTask;
        }
        private void DoWork(object state)
        {
            var count = Interlocked.Increment(ref _executionCount);

            var factory = new ConnectionFactory();
            factory.UserName = "guest";
            factory.Password = "guest";
            var endpoints = new System.Collections.Generic.List<AmqpTcpEndpoint> {
                new AmqpTcpEndpoint("hostname"),
                new AmqpTcpEndpoint("localhost")
            };
            using (var connection = factory.CreateConnection(endpoints))
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "News",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += async (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        NewsModel n = JsonConvert.DeserializeObject<NewsModel>(Encoding.UTF8.GetString(body));
                        Console.WriteLine(" [x] Received \n {0}", n);
                        CreateNewsCommand command = new CreateNewsCommand()
                        {
                            Title = n.Title,
                            Url = n.Url
                        };
                        using (var scope = _serviceScopeFactory.CreateScope())
                        {
                            _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                            await _mediator.Send(command);
                        }
                         
                    };
                    channel.BasicConsume(queue: "News",
                                         autoAck: true,
                                         consumer: consumer);
                    
                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
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
