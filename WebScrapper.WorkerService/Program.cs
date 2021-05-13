using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenQA.Selenium.Chrome;
using WebScrapper.Core;
using WebScrapper.Core.Interfaces;

namespace WebScrapper.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<ScrappingWorker>();
                    services.Add(new ServiceDescriptor(typeof(IScrapperService),
                        new GNewsScrapperService(new ChromeDriver(@"D:\Softwares\ChromeDriver"))));
                });
    }
}
