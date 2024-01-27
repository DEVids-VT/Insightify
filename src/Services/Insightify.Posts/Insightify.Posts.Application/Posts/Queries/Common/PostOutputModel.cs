using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Insightify.Posts.Application.Common.Mapping;
using Insightify.Posts.Domain.Posts.Models;

namespace Insightify.Posts.Application.Posts.Queries.Common
{
    public class PostOutputModel : IMapFrom<Post>
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string AuthorId { get; set; } = default!;
        public DateTime UploadDate { get; set; }
        public string? ImageUrl { get; set; }
        public int LikeCount { get; set; }
        public int SaveCount { get; set; }
        public int CommentCount { get; set; }

        public virtual void Mapping(Profile mapper)
            => mapper.CreateMap<Post, PostOutputModel>()
                .ForMember(p => p.LikeCount, cfg => cfg.MapFrom(p => p.TotalLikes))
                .ForMember(p => p.SaveCount, cfg => cfg.MapFrom(p => p.TotalSaves))
                .ForMember(p => p.CommentCount, cfg => cfg.MapFrom(p => p.TotalComments));
    }
}
