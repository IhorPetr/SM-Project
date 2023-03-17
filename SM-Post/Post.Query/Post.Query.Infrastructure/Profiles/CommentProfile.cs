using AutoMapper;
using Post.Common.Events.Comment;
using Post.Query.Domain.Entities;

namespace Post.Query.Infrastructure.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<CommentAddedEvent, CommentEntity>()
                .ForMember(x => x.PostId, opt => opt.MapFrom(d => d.Id))
                .ForMember(x => x.Edited, opt => opt.NullSubstitute(false));
        }
    }
}
