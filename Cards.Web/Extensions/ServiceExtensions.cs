using Cards.Domain.Contracts;
using Cards.Persistence;
using Cards.Persistence.Repositories;
using Cards.Services;
using Cards.Services.Abstraction;
using Cards.Services.Services;
using Microsoft.EntityFrameworkCore;

namespace Cards.Web.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureNpgsqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContextPool<RepositoryDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        public static void ConfigureAutoMapper(this IServiceCollection services) =>
            services.AddAutoMapper(typeof(MappingProfile));

        public static void ConfigureCardService(this IServiceCollection services) =>
            services.AddScoped<ICardService, CardService>();

        public static void ConfigureCardRepository(this IServiceCollection services) =>
           services.AddScoped<ICardRepository, CardRepository>();

        public static void ConfigureAppUserRepository(this IServiceCollection services) =>
           services.AddScoped<IAppUserRepository, AppUserRepository>(); 

        public static void ConfigureUnitOfWorkRepository(this IServiceCollection services) =>
           services.AddScoped<IUnitOfWork, UnitOfWork>();

    }
}
