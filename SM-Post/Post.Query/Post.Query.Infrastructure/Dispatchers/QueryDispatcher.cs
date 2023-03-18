using CQRS.Core.Infrastructure;
using CQRS.Core.Queries;
using Post.Query.Domain.Entities;
using Post.Query.Domain.Queries;
using Post.Query.Infrastructure.Handlers;

namespace Post.Query.Infrastructure.Dispatchers
{
    public class QueryDispatcher : IQueryDispatcher<PostEntity>
    {
        private readonly Dictionary<Type, Func<BaseQuery, Task<List<PostEntity>>>> _handlers = new();

        public QueryDispatcher(IQueryHandler queryHandler)
        {
            RegisterHandler<FindAllPostsQuery>(queryHandler.HandleAsync);
            RegisterHandler<FindPostByIdQuery>(queryHandler.HandleAsync);
            RegisterHandler<FindPostsByAuthorQuery>(queryHandler.HandleAsync);
            RegisterHandler<FindPostsWithCommentsQuery>(queryHandler.HandleAsync);
            RegisterHandler<FindPostsWithLikesQuery>(queryHandler.HandleAsync);
        }

        public async Task<List<PostEntity>> SendAsync(BaseQuery query)
        {
            if (!_handlers.TryGetValue(query.GetType(), out Func<BaseQuery, Task<List<PostEntity>>> handler))
            {
                throw new ArgumentNullException(nameof(handler), "No query handler was registered");
            }

            return await handler(query);
        }

        private void RegisterHandler<TQuery>(Func<TQuery, Task<List<PostEntity>>> handler) where TQuery : BaseQuery
        {
            if (_handlers.ContainsKey(typeof(TQuery)))
            {
                throw new IndexOutOfRangeException($"You can`t register the same query twice: {nameof(TQuery)}");
            }

            _handlers.Add(typeof(TQuery), x => handler((TQuery)x));
        }
    }
}
