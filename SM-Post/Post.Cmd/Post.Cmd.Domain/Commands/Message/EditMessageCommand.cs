using CQRS.Core.Commands;

namespace Post.Cmd.Domain.Commands.Message
{
    public class EditMessageCommand : BaseCommand
    {
        public string Message { get; set; }
    }
}
