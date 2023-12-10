using DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tests
{
    public class CustomWebApplicationFactory<TProgram>
        : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<ApplicationContext>));

                if (dbContextDescriptor is not null)
                    services.Remove(dbContextDescriptor);

                services.AddDbContext<ApplicationContext>(opts =>
                {
                    opts.UseInMemoryDatabase("TestDB");
                });
            });

            builder.UseEnvironment("Development");
        }
    }
}
