using CQRS.Core.Commands;

namespace Post.Cmd.Api.Commands.Message
{
    public class EditMessageCommand : BaseCommand
    {
        public string Message { get; set; }
    }
}
