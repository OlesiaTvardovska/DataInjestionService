using GraphQL;
using GraphQL.Types;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebScrapper.Api.GraphQL.Types;
using WebScrapper.Application.Features.MarkedNewsFeatures.Queries;
using WebScrapper.Application.Features.NewsFeatures.Queries;
using WebScrapper.Core.Models;

namespace WebScrapper.Api.GraphQL.Queries
{
    public class NewsQuery : ObjectGraphType
    {
        private IMediator _mediator;

        public NewsQuery(IMediator mediator)
        {
            Name = "Query";
            _mediator = mediator;

            Field<ListGraphType<NewsObject>>("news_list", resolve:
                context => GetNewsList());

            FieldAsync<NewsObject, NewsModel>(
                "news",
                "Gets a news by its unique identifier.",
                new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>>
                    {
                        Name = "id",
                        Description = "The unique id of the news."
                    }),
                context => GetNewsById(context.GetArgument("id", 0)));

            Field<ListGraphType<NewsObject>>("unmarked_news", "Gets unmarked news for unique user.",
                new QueryArguments(
                new QueryArgument<NonNullGraphType<IdGraphType>>
                {
                    Name = "user_id",
                    Description = "The unique id of the user."
                }),
                resolve:
                context => GetUnmarkedNews(context.GetArgument("user_id", "")));
        }

        private async Task<List<NewsModel>> GetNewsList()
        {
            return await _mediator.Send(new GetNewsQuery());
        }

        private async Task<NewsModel> GetNewsById(int id)
        {
            return await _mediator.Send(new GetNewsByIdQuery { Id = id });
        }

        private async Task<List<NewsModel>> GetUnmarkedNews(string userId)
        {
            var markedNewsIds = (await _mediator.Send(new GetMarkedNewsQuery { Id = userId })).Select(x => x.NewsId);
            return (await GetNewsList()).Where(x => !markedNewsIds.Contains(x.Id)).ToList();

        }
    }
}
