using CQRS.Core.Queries;

namespace CQRS.Core.Infrastructure
{
    public interface IQueryDispatcher<TEntity>
    {
        Task<List<TEntity>> SendAsync(BaseQuery query);
    }
}
