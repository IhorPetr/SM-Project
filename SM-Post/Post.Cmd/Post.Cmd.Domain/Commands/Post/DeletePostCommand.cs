using CQRS.Core.Commands;

namespace Post.Cmd.Domain.Commands.Post
{
    public class DeletePostCommand : BaseCommand
    {
        public string UserName { get; set; }
    }
}
