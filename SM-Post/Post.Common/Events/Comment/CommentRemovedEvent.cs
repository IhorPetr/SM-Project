namespace Post.Common.Events.Comment
{
    public class CommentRemovedEvent : CommentBaseEvent
    {
        public CommentRemovedEvent() : base(nameof(CommentRemovedEvent))
        {
        }
    }
}
