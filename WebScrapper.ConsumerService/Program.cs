using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebScrapper.Application;
using WebScrapper.DAL;

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
