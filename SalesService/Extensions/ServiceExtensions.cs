using DataAccess;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Interfaces;

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

        public static void ConfigureLoggerManager(this IServiceCollection services)
            => services.AddSingleton<ILoggerManager, LoggerManager>();
    }
}
