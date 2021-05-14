using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebScrapper.Api.GraphQL.Queries;

namespace WebScrapper.Api.GraphQL
{
    public class MainSchema : Schema
    {
        public MainSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<NewsQuery>();
        }
    }
}
