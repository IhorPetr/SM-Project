using CQRS.Core.Commands;

namespace Post.Cmd.Api.Commands.Comment
{
    public class EditCommentCommand : BaseCommentCommand
    {
        public string Comment { get; set; }
        public Guid CommentId { get; set; }
    }
}
