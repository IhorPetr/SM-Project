using CQRS.Core.Commands;

namespace Post.Cmd.Api.Commands.Post
{
    public class DeletePostCommand : BaseCommand
    {
        public string UserName { get; set; }
    }
}
