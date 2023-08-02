using DataAccess;
using DataAccess.Interfaces;

namespace Web.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureUnitOdWork(this IServiceCollection services)
            => services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
