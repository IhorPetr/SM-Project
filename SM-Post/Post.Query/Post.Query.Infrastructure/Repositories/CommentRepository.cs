using Microsoft.EntityFrameworkCore;
using Post.Query.Domain.Entities;
using Post.Query.Domain.Repositories;
using Post.Query.Infrastructure.DataAccess;

namespace Post.Query.Infrastructure.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DatabaseContextFactory _databaseContextFactory;

        public CommentRepository(DatabaseContextFactory databaseContextFactory)
        {
            _databaseContextFactory = databaseContextFactory;
        }

        public async Task CreateAsync(CommentEntity entity)
        {
            using DatabaseContext databaseContext = _databaseContextFactory.CreateDbContext();
            databaseContext.Comments.Add(entity);
            _ = await databaseContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid commentId)
        {
            using DatabaseContext databaseContext = _databaseContextFactory.CreateDbContext();
            var post = await GetByIdAsync(commentId);
            if (post == null)
            {
                return;
            }
            databaseContext.Comments.Remove(post);
            _ = await databaseContext.SaveChangesAsync();
        }

        public async Task<CommentEntity> GetByIdAsync(Guid commentId)
        {
            using DatabaseContext databaseContext = _databaseContextFactory.CreateDbContext();
            return await databaseContext.Comments.FirstOrDefaultAsync(r => r.PostId == commentId);
        }

        public async Task UpdateAsync(CommentEntity entity)
        {
            using DatabaseContext databaseContext = _databaseContextFactory.CreateDbContext();
            databaseContext.Comments.Update(entity);
            _ = await databaseContext.SaveChangesAsync();
        }
    }
}
