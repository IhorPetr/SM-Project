namespace Post.Cmd.Domain.Commands.Comment
{
    public class EditCommentCommand : BaseCommentCommand
    {
        public string Comment { get; set; }
        public Guid CommentId { get; set; }
    }
}
