using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using WebScrapper.Core.Models;

namespace WebScrapper.Sender
{
    public class RabbitMQSender
    {
        public void SendData(List<NewsModel> newsList)
        {
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

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    foreach (var news in newsList)
                    {
                        var body = Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(news));

                        channel.BasicPublish(exchange: "",
                                             routingKey: "News",
                                             basicProperties: properties,
                                             body: body);
                        Console.WriteLine(" [x] Sent \n {0}", news.Title);
                    }
                }
            }
        }
    }
}
