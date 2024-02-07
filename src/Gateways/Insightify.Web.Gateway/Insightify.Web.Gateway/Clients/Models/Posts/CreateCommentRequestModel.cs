using AutoMapper;
using Insightify.Web.Gateway.Infrastructure.Mapping;
using Insightify.Web.Gateway.Models.Posts;

namespace Insightify.Web.Gateway.Clients.Models.Posts
{
    public class CreateCommentRequestModel : IMapFrom<CreateCommentInputModel>
    {
        public int Id { get; set; }
        public string Content { get; set; } = default!;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCommentInputModel, CreateCommentRequestModel>()
                .ForMember(x => x.Id, opts => opts.MapFrom(src => src.PostId));
        }
    }
}
