namespace Post.Common.Events.Comment
{
    public class CommentUpdatedEvent : CommentBaseEvent
    {
        public string Comment { get; set; }
        public string UserName { get; set; }
        public DateTime EditDate { get; set; }

        public CommentUpdatedEvent() : base(nameof(CommentUpdatedEvent))
        {
        }
    }
}
