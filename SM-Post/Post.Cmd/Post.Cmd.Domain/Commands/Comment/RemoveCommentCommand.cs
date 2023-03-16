namespace Post.Cmd.Domain.Commands.Comment
{
    public class RemoveCommentCommand : BaseCommentCommand
    {
        public Guid CommentId { get; set; }
    }
}
