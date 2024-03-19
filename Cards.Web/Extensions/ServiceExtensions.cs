using Cards.Domain.Contracts;
using Cards.Domain.Entities;
using Cards.Persistence;
using Cards.Persistence.Repositories;
using Cards.Services;
using Cards.Services.Abstraction;
using Cards.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

        public static void ConfigureAuthService(this IServiceCollection services) =>
            services.AddScoped<IAuthenticationService, AuthenticationService>();

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<AppUser, Role>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 10;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<RepositoryDbContext>()
            .AddDefaultTokenProviders();
        }      

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");

            //var secret = Encoding.UTF8.GetBytes(configuration.GetSection("JwtSettings:Secret").Value);
            byte[] secret = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("CardAPISecret"));

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(secret)
                };
            });
        }

    }
}
