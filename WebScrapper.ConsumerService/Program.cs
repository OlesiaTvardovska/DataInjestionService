using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenQA.Selenium.Chrome;
using System;
using System.Reflection;
using WebScrapper.Application;
using WebScrapper.Application.Interfaces;
using WebScrapper.Core;
using WebScrapper.Core.Interfaces;
using WebScrapper.DAL;
using WebScrapper.DAL.Context;

namespace WebScrapper.ConsumerService
{
    public class Program
    {

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    var configuration = hostContext.Configuration;
                    services.AddApplication();
                    services.AddPersistence(configuration);

                });
    }
}
