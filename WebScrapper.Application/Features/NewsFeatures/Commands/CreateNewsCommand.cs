using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebScrapper.Application.Interfaces;
using WebScrapper.Core.Entities;

namespace WebScrapper.Application.Features.NewsFeatures.Commands
{
    public class CreateNewsCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Url { get; set; }

        public class CreateNewsCommandHandler : IRequestHandler<CreateNewsCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public CreateNewsCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreateNewsCommand command, CancellationToken cancellationToken)
            {
                var news = new NewsEntity
                {
                    Title = command.Title,
                    Url = command.Url
                };
                await _context.News.AddAsync(news, cancellationToken: cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return news.Id;
            }
        }
    }
}
