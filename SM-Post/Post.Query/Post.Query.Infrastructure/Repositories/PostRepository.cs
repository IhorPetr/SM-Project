using Microsoft.EntityFrameworkCore;
using Post.Query.Domain.Entities;
using Post.Query.Domain.Repositories;
using Post.Query.Infrastructure.DataAccess;

namespace Post.Query.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DatabaseContextFactory _databaseContextFactory;

        public PostRepository(DatabaseContextFactory databaseContextFactory)
        {
            _databaseContextFactory = databaseContextFactory;
        }

        public async Task CreateAsync(PostEntity entity)
        {
            using DatabaseContext databaseContext = _databaseContextFactory.CreateDbContext();
            databaseContext.Posts.Add(entity);
            _ = await databaseContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid postId)
        {
            using DatabaseContext databaseContext = _databaseContextFactory.CreateDbContext();
            var post = await GetByIdAsync(postId);
            if (post == null)
            {
                return;
            }
            databaseContext.Posts.Remove(post);
            _ = await databaseContext.SaveChangesAsync();
        }

        public async Task<PostEntity> GetByIdAsync(Guid postId)
        {
            using DatabaseContext databaseContext = _databaseContextFactory.CreateDbContext();
            return await databaseContext.Posts.Include(p => p.Comments).FirstOrDefaultAsync(r => r.PostId == postId);
        }

        public async Task<List<PostEntity>> ListAllAsync()
        {
            using DatabaseContext databaseContext = _databaseContextFactory.CreateDbContext();
            return await databaseContext.Posts.AsNoTracking().Include(p => p.Comments).AsNoTracking().ToListAsync();
        }

        public async Task<List<PostEntity>> ListByAuthorAsync(string author)
        {
            using DatabaseContext databaseContext = _databaseContextFactory.CreateDbContext();
            return await databaseContext.Posts.AsNoTracking().Include(p => p.Comments).AsNoTracking()
                .Where(p => p.Author == author).ToListAsync();
        }

        public async Task<List<PostEntity>> ListWithCommetsAsync()
        {
            using DatabaseContext databaseContext = _databaseContextFactory.CreateDbContext();
            return await databaseContext.Posts.AsNoTracking().Include(p => p.Comments).AsNoTracking()
                .Where(p => p.Comments != null && p.Comments.Any()).ToListAsync();
        }

        public async Task<List<PostEntity>> ListWithLikeAsync(int numberOfLikes)
        {
            using DatabaseContext databaseContext = _databaseContextFactory.CreateDbContext();
            return await databaseContext.Posts.AsNoTracking().Include(p => p.Comments).AsNoTracking()
                .Where(p => p.Likes == numberOfLikes).ToListAsync();
        }

        public async Task UpdateAsync(PostEntity entity)
        {
            using DatabaseContext databaseContext = _databaseContextFactory.CreateDbContext();
            databaseContext.Posts.Update(entity);
            _ = await databaseContext.SaveChangesAsync();
        }
    }
}
