using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Post.Query.Infrastructure.DataAccess
{
    public static class DatabaseExtensions
    {
        public static void InitDatabase(this IHost app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            context.Database.EnsureCreated();
        }
    }
}
