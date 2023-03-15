using CQRS.Core.Events;

namespace Post.Common.Events.Message
{
    public class MessageUpdatedEvent : BaseEvent
    {
        public string Message { get; set; }

        public MessageUpdatedEvent() : base(nameof(MessageUpdatedEvent))
        {
        }
    }
}
