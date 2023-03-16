using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Post.Query.Domain.Entities;

namespace Post.Query.Infrastructure.DataAccess.EntityTypeConfiguration
{
    internal class PostEntityTypeConfiguration : IEntityTypeConfiguration<PostEntity>
    {
        public void Configure(EntityTypeBuilder<PostEntity> builder)
        {
            builder.ToTable("Post").HasKey(x => x.PostId);
        }
    }
}
