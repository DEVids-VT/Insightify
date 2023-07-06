using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Framework.MongoDb.Abstractions.Interfaces;
using Insightify.Posts.Domain.Common;
using Insightify.Posts.Domain.Posts.Models;

namespace Insightify.Posts.Application.Posts
{
    public interface IPostQueryRepository
    {
        Task<IPagedList<TOutputModel>> GetPosts<TOutputModel>(
            Specification<Post> postSpecification, 
            int pageNumber,
            int pageSize = 10, 
            CancellationToken cancellationToken = default);

        Task<IEnumerable<TCommentOutputModel>> GetComments<TCommentOutputModel>(
            int id, 
            CancellationToken cancellationToken = default);

        Task<IEnumerable<TLikeOutputModel>> GetLikes<TLikeOutputModel>(int id,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<TSaveOutputModel>> GetSaves<TSaveOutputModel>(int id,
            CancellationToken cancellationToken = default);
    }
}
