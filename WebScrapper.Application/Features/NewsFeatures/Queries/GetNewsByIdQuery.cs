using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebScrapper.Application.Interfaces;
using WebScrapper.Core.Models;

namespace WebScrapper.Application.Features.NewsFeatures.Queries
{
    public class GetNewsByIdQuery : IRequest<NewsModel>
    {
        public int Id { get; set; }

        public class GetNewsByIdQueryHandler : IRequestHandler<GetNewsByIdQuery, NewsModel>
        {
            private readonly IApplicationDbContext _context;
            public GetNewsByIdQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<NewsModel> Handle(GetNewsByIdQuery query, CancellationToken cancellationToken)
            {
                var newsEntity = await _context.News.FindAsync(query.Id);
                if(newsEntity == null)
                {
                    return null;
                }
                return new NewsModel
                {
                    Title = newsEntity.Title,
                    Url = newsEntity.Url, 
                    Id = newsEntity.Id
                };
            }
        }
    }
}
