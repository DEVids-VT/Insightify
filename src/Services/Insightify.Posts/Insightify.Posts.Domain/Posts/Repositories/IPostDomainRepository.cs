using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Posts.Domain.Common;
using Insightify.Posts.Domain.Posts.Models;

namespace Insightify.Posts.Domain.Posts.Repositories
{
    public interface IPostDomainRepository : IDomainRepository<Post>
    {
        Task<Post?> Find(int id, CancellationToken cancellationToken = default);
        Task<bool> Delete(int id, CancellationToken cancellationToken = default);
    }
}
