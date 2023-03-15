using CQRS.Core.Commands;

namespace Post.Cmd.Api.Commands.Comment
{
    public class RemoveCommentCommand : BaseCommentCommand
    {
        public Guid CommentId { get; set; }
    }
}
