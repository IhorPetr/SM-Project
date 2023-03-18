using CQRS.Core.Queries;

namespace Post.Query.Domain.Queries
{
    public class FindPostsByAuthorQuery : BaseQuery
    {
        public string Author { get; set; }
    }
}
