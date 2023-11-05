using SalesService.Extensions;
using Services.Interfaces;
using Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureLoggerManager();

builder.Services.ConfigureDbContext(builder.Configuration);

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.ConfigureServiceManager();

builder.Services.ConfigureUnitOfWork();

builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.AddControllers()
    .AddApplicationPart(typeof(Controllers.AssemblyReference).Assembly);

builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.

var logger = app.Services.GetService<ILoggerManager>();

app.UseSwagger();
app.UseSwaggerUI();

app.ConfigureExceptionHandler(logger);

// app.UseHttpsRedirection();

app.UseAuthentication();
app.UseMiddleware<CheckUserStatusMiddleware>();
app.UseAuthorization();


app.MapControllers();

app.Run();
