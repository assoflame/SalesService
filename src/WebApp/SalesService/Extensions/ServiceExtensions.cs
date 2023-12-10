using DataAccess;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services;
using Services.Interfaces;
using System.Text;

namespace Web.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureUnitOfWork(this IServiceCollection services)
            => services.AddScoped<IUnitOfWork, UnitOfWork>();

        public static void ConfigureServiceManager(this IServiceCollection services)
            => services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
            => services.AddDbContext<ApplicationContext>(opts =>
                opts.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = jwtSettings["validIssuer"],
                        ValidAudience = jwtSettings["validAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["secretKey"]))
                    };
                });
        }

        public static void ConfigureCors(this IServiceCollection services)
            => services.AddCors(opts =>
            {
                opts.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
    }
}
