namespace Post.Common.Events.Comment
{
    public class CommentAddedEvent : CommentBaseEvent
    {
        public string Comment { get; set; }
        public string UserName { get; set; }
        public DateTime CommentDate { get; set; }

        public CommentAddedEvent() : base(nameof(CommentAddedEvent))
        {
        }
    }
}
