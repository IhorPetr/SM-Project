using CQRS.Core.Events;

namespace Post.Common.Events.Comment
{
    public abstract class CommentBaseEvent : BaseEvent
    {
        public Guid CommentId { get; set; }

        public CommentBaseEvent(string Type) : base(Type)
        {
        }
    }
}
