using Post.Common.DTOs;
using Post.Query.Domain.Entities;

namespace Post.Query.Api.DTOs
{
    public class PostLookupResponce : BaseResponce
    {
        public List<PostEntity> Posts { get; set; }
    }
}
