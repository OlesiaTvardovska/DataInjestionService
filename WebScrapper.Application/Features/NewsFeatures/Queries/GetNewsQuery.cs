using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebScrapper.Application.Interfaces;
using WebScrapper.Core.Models;

namespace WebScrapper.Application.Features.NewsFeatures.Queries
{
    public class GetNewsQuery : IRequest<List<NewsModel>>
    {
        public class GetNewsQueryHandler : IRequestHandler<GetNewsQuery, List<NewsModel>>
        {
            private readonly IApplicationDbContext _context;
            public GetNewsQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<List<NewsModel>> Handle(GetNewsQuery query, CancellationToken cancellationToken)
            {
                var newsList = await _context.News.AsNoTracking().Select(x => new NewsModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Url = x.Url
                }).ToListAsync();

                return newsList;
            }
        }
    }
}
