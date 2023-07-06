using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insightify.Posts.Application.Common.Mapping;
using Insightify.Posts.Application.Posts.Queries.Common;
using Insightify.Posts.Domain.Posts.Models;

namespace Insightify.Posts.Application.Posts.Queries.Search
{
    public class SearchPostOutputModel : PostOutputModel, IMapFrom<Post> { }
}
