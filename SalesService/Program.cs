using DataAccess;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Interfaces;
using Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder().Build();

// Add services to the container.

builder.Services.AddControllers()
    .AddApplicationPart(typeof(Controllers.AssemblyReference).Assembly);
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseNpgsql("Host=localhost;Port=5432;Database=SalesService;Username=postgres;Password=password");
});

builder.Services.AddScoped<IServiceManager, ServiceManager>();

builder.Services.ConfigureUnitOfWork();


builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
