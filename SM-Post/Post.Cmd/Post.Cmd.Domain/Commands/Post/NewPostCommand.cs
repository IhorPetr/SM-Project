using CQRS.Core.Commands;

namespace Post.Cmd.Domain.Commands.Post
{
    public class NewPostCommand : BaseCommand
    {
        public string Author { get; set; }
        public string Message { get; set; }
    }
}
