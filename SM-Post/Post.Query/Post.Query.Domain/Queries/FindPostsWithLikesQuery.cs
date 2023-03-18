using CQRS.Core.Queries;

namespace Post.Query.Domain.Queries
{
    public class FindPostsWithLikesQuery : BaseQuery
    {
        public int NumberOfLikes { get; set; }
    }
}
