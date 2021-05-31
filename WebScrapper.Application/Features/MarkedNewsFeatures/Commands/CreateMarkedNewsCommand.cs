using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebScrapper.Application.Interfaces;
using WebScrapper.Core.Entities;

namespace WebScrapper.Application.Features.MarkedNewsFeatures.Commands
{
    public class CreateMarkedNewsCommand : IRequest<int>
    {
        public int NewsId { get; set; }

        public bool IsLiked { get; set; }

        public string UserId { get; set; }

        public class CreateMarkedNewsCommandHandler : IRequestHandler<CreateMarkedNewsCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public CreateMarkedNewsCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreateMarkedNewsCommand command, CancellationToken cancellationToken)
            {
                var markedNews = new MarkedItemEntity
                {
                    NewsId = command.NewsId,
                    IsLiked = command.IsLiked, 
                    UserId = command.UserId
                };
                await _context.MarkedNews.AddAsync(markedNews, cancellationToken: cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return markedNews.Id;
            }
        }
    }
}
