using Cards.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Cards.Web.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureNpgsqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContextPool<RepositoryDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

    }
}
