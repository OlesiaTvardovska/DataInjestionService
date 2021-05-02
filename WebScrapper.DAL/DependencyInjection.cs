using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using WebScrapper.Application.Interfaces;
using WebScrapper.DAL.Context;

namespace WebScrapper.DAL
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseNpgsql(connectionString, serverOptions =>
                    {
                        serverOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    });
                });

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
        }
    }
}
