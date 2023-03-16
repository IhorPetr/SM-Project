using CQRS.Core.Commands;

namespace Post.Cmd.Domain.Commands.Comment
{
    public abstract class BaseCommentCommand : BaseCommand
    {
        public string UserName { get; set; }
    }
}
