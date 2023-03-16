using Post.Cmd.Domain.Commands.Comment;
using Post.Cmd.Domain.Commands.Message;
using Post.Cmd.Domain.Commands.Post;

namespace Post.Cmd.Domain.Commands
{
    public interface ICommandHandler
    {
        Task HandleAsync(NewPostCommand command);
        Task HandleAsync(EditMessageCommand command);
        Task HandleAsync(LikePostCommand command);
        Task HandleAsync(AddCommentCommand command);
        Task HandleAsync(EditCommentCommand command);
        Task HandleAsync(RemoveCommentCommand command);
        Task HandleAsync(DeletePostCommand command);
    }
}
