using CQRS.Core.Handlers;
using Post.Cmd.Domain.Commands.Comment;
using Post.Cmd.Domain.Commands.Message;
using Post.Cmd.Domain.Commands.Post;
using Post.Cmd.Domain.Aggregates;

namespace Post.Cmd.Domain.Commands
{
    public class CommandHandler : ICommandHandler
    {
        private readonly IEventSourcingHandler<PostAggregate> _eventSourcingHandler;

        public CommandHandler(IEventSourcingHandler<PostAggregate> eventSourcingHandler)
        {
            _eventSourcingHandler = eventSourcingHandler;
        }

        public async Task HandleAsync(NewPostCommand command)
        {
            var aggregate = new PostAggregate(command.Id, command.Author, command.Message);
            await _eventSourcingHandler.SaveAsync(aggregate);
        }

        public async Task HandleAsync(EditMessageCommand command)
        {
           await EditActionOnAggregate(command.Id, (agr) => agr.EditMessage(command.Message));
        }

        public async Task HandleAsync(LikePostCommand command)
        {
            await EditActionOnAggregate(command.Id, (agr) => agr.LikePost());
        }

        public async Task HandleAsync(AddCommentCommand command)
        {
            await EditActionOnAggregate(command.Id, (agr) => agr.AddComment(command.Comment, command.UserName));
        }

        public async Task HandleAsync(EditCommentCommand command)
        {
            await EditActionOnAggregate(command.Id, (agr) => agr.EditComment(command.CommentId, command.Comment, command.UserName));
        }

        public async Task HandleAsync(RemoveCommentCommand command)
        {
            await EditActionOnAggregate(command.Id, (agr) => agr.RemoveComment(command.CommentId, command.UserName));
        }

        public async Task HandleAsync(DeletePostCommand command)
        {
            await EditActionOnAggregate(command.Id, (agr) => agr.DeletePost(command.UserName));
        }

        public async Task HandleAsync(RestoreReadDbCommand command)
        {
            await _eventSourcingHandler.RepublishEventAsync();
        }

        private async Task EditActionOnAggregate(Guid Id, Action<PostAggregate> postAggregateAction)
        {
            var aggregate = await _eventSourcingHandler.GetByIdAsync(Id);
            postAggregateAction(aggregate);

            await _eventSourcingHandler.SaveAsync(aggregate);
        }
    }
}
