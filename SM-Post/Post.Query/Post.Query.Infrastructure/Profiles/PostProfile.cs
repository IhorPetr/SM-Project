using AutoMapper;
using Post.Common.Events.Post;
using Post.Query.Domain.Entities;

namespace Post.Query.Infrastructure.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<PostCreatedEvent, PostEntity>()
                .ForMember(x => x.PostId, opt => opt.MapFrom(d => d.Id));
        }
    }
}
