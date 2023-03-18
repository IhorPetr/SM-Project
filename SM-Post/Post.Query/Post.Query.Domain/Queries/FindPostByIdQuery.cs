using CQRS.Core.Queries;

namespace Post.Query.Domain.Queries
{
    public class FindPostByIdQuery : BaseQuery
    {
        public Guid Id { get; set; }
    }
}
