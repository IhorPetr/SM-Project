using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Post.Query.Infrastructure.Extensions
{
    public static class ServicesDIExtensions
    {
        public static IServiceCollection AddAutoMaping(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
