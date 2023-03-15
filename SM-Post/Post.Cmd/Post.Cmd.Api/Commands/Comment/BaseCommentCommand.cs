using CQRS.Core.Commands;

namespace Post.Cmd.Api.Commands.Comment
{
    public abstract class BaseCommentCommand : BaseCommand
    {
        public string UserName { get; set; }
    }
}
