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

namespace WebScrapper.Application.Features.MarkedNewsFeatures.Queries
{
    public class GetMarkedNewsQuery : IRequest<List<MarkedNewsModel>>
    {
        public string Id { get; set; }

        public class GetMarkedNewsQueryHandler : IRequestHandler<GetMarkedNewsQuery, List<MarkedNewsModel>>
        {
            private readonly IApplicationDbContext _context;
            public GetMarkedNewsQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<List<MarkedNewsModel>> Handle(GetMarkedNewsQuery query, CancellationToken cancellationToken)
            {
                var newsList = await _context.MarkedNews.AsNoTracking().Select(x => new MarkedNewsModel
                {
                    Id = x.Id,
                    NewsId = x.NewsId,
                    UserId = x.UserId,
                    IsLiked = x.IsLiked
                }).Where(x => x.UserId == query.Id).ToListAsync();

                return newsList;
            }
        }
    }
}
