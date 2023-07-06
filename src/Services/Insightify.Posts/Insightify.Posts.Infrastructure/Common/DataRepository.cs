using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Posts.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Insightify.Posts.Infrastructure.Common
{
    internal class DataRepository<TDbContext, TEntity> : IDomainRepository<TEntity>
        where TDbContext : DbContext
        where TEntity : class, IAggregateRoot
    {
        protected DataRepository(TDbContext db) => this.Data = db;
        protected TDbContext Data { get; }
        protected IQueryable<TEntity> All() => this.Data.Set<TEntity>();
        public async Task Save(TEntity entity, CancellationToken cancellationToken = default)
        {
            this.Data.Update(entity);
            await this.Data.SaveChangesAsync(cancellationToken);
        }
    }
}
