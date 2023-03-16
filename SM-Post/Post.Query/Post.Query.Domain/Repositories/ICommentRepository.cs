using Post.Query.Domain.Entities;

namespace Post.Query.Domain.Repositories
{
    public interface ICommentRepository
    {
        Task CreateAsync(CommentEntity entity);
        Task UpdateAsync(CommentEntity entity);
        Task DeleteAsync(Guid commentId);
        Task<CommentEntity> GetByIdAsync(Guid commentId);
    }
}
