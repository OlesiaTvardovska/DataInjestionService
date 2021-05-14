using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using WebScrapper.Api.GraphQL;
using MediatR;
using WebScrapper.Application;
using WebScrapper.DAL;
using System.Web.Http.Dependencies;
using Splat;

namespace WebScrapper.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebScrapper.Api", Version = "v1" });
            });

            services.AddScoped<MainSchema>();
            services.AddGraphQL().AddGraphTypes(ServiceLifetime.Scoped).AddSystemTextJson();
            services.AddApplication();
            services.AddPersistence(Configuration);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebScrapper.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseGraphQL<MainSchema>();
            app.UseGraphQLPlayground();

        }
    }
}
