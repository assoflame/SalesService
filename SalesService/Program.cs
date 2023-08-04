using DataAccess;
using Microsoft.EntityFrameworkCore;
using SalesService.Extensions;
using Services;
using Services.Interfaces;
using Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureLoggerManager();

builder.Services.ConfigureDbContext(builder.Configuration);

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.ConfigureServiceManager();

builder.Services.ConfigureUnitOfWork();

builder.Services.AddControllers()
    .AddApplicationPart(typeof(Controllers.AssemblyReference).Assembly);


var app = builder.Build();

// Configure the HTTP request pipeline.

var logger = app.Services.GetService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
