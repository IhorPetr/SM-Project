using CQRS.Core.Commands;

namespace Post.Cmd.Api.Commands.Comment
{
    public class AddCommentCommand : BaseCommentCommand
    {
        public string Comment { get; set; }
    }
}
