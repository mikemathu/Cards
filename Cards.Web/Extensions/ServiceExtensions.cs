using Cards.Domain.Contracts;
using Cards.Domain.Entities;
using Cards.Frontend;
using Cards.Persistence;
using Cards.Persistence.Repositories;
using Cards.Services;
using Cards.Services.Abstraction;
using Cards.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace Cards.Web.Extensions
{
    public static class ServiceExtensions
    {        
        public static void ConfigureApiBehaviorOptions(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }
        public static void ConfigureCors(this IServiceCollection services) =>
             services.AddCors(options =>
             {
                 options.AddPolicy("CorsPolicy", builder =>
                 builder.AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader());
             });
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
                //options.Password.RequireDigit = true;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<RepositoryDbContext>()
            .AddDefaultTokenProviders();
        }     
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");

            var secret = Encoding.UTF8.GetBytes(configuration.GetSection("JwtSettings:Secret").Value);
            //byte[] secret = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("CardAPISecret"));

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
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(secret)
                };
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Cards API",
                    Version = "v1",
                    Description = "An ASP.NET Core Web API for managing tasks",
                });
                var xmlFile = $"{typeof(Presentation.Controllers.CardsController).Assembly.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                s.IncludeXmlComments(xmlPath);

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Place to add JWT with Bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement()
                 {
                     {
                         new OpenApiSecurityScheme
                         {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer",
                         },
                         new List<string>()
                     }
                 });
            });
        }
        public static void UseCardsFrontendStaticFiles(this IApplicationBuilder app)
        {

            var embeddedFileProvider = new EmbeddedFileProvider(
                typeof(FrontendAssemblyReference).GetTypeInfo().Assembly, "Cards.Frontend.wwwroot");

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = embeddedFileProvider
            });
        }


    }
}
