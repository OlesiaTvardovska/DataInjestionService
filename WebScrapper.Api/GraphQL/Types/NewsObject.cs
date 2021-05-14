using GraphQL.Types;
using WebScrapper.Core.Entities;
using WebScrapper.Core.Models;

namespace WebScrapper.Api.GraphQL.Types
{
    public class NewsObject : ObjectGraphType<NewsModel>
    {
        public NewsObject()
        {
            Description = "A news in the collection";

            Field(m => m.Id);
            Field(m => m.Title).Description("Title");
            Field(m => m.Url).Description("Url");
        }
    }
}
