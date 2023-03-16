using Microsoft.EntityFrameworkCore;
using Post.Query.Domain.Entities;
using System.Reflection;


namespace Post.Query.Infrastructure.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DbSet<PostEntity> Posts { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }

        public DatabaseContext(DbContextOptions options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
