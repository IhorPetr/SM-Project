using CQRS.Core.Commands;
using CQRS.Core.Infrastructure;
using Post.Cmd.Domain.Commands;
using Post.Cmd.Domain.Commands.Comment;
using Post.Cmd.Domain.Commands.Message;
using Post.Cmd.Domain.Commands.Post;

namespace Post.Cmd.Infrastructure.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly Dictionary<Type, Func<BaseCommand, Task>> _handlers = new ();

        public CommandDispatcher(ICommandHandler commandHandler)
        {
            RegisterHandler<NewPostCommand>(commandHandler.HandleAsync);
            RegisterHandler<EditMessageCommand>(commandHandler.HandleAsync);
            RegisterHandler<LikePostCommand>(commandHandler.HandleAsync);
            RegisterHandler<AddCommentCommand>(commandHandler.HandleAsync);
            RegisterHandler<EditCommentCommand>(commandHandler.HandleAsync);
            RegisterHandler<RemoveCommentCommand>(commandHandler.HandleAsync);
            RegisterHandler<DeletePostCommand>(commandHandler.HandleAsync);
        }

        public async Task SendAsync(BaseCommand command)
        {
            if(!_handlers.TryGetValue(command.GetType(), out Func<BaseCommand, Task> handler))
            {
                throw new ArgumentNullException(nameof(handler), "No command handler was registered");
            }

            await handler(command);
        }

        private void RegisterHandler<T>(Func<T, Task> handler) where T : BaseCommand
        {
            if (_handlers.ContainsKey(typeof(T)))
            {
                throw new IndexOutOfRangeException($"You can`t register the same command twice: {nameof(T)}");
            }

            _handlers.Add(typeof(T), x => handler((T)x));
        }
    }
}
