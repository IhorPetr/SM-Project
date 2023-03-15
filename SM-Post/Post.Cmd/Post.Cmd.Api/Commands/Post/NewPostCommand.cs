using CQRS.Core.Commands;

namespace Post.Cmd.Api.Commands.Post
{
    public class NewPostCommand : BaseCommand
    {
        public string Author { get; set; }
        public string Message { get; set; }
    }
}
