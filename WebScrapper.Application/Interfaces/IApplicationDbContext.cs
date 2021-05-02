using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebScrapper.Core.Entities;

namespace WebScrapper.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<NewsEntity> News { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
