using SalesService.Extensions;
using Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureDbContext(builder.Configuration);

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.ConfigureServiceManager();

builder.Services.ConfigureUnitOfWork();

builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.AddControllers()
    .AddApplicationPart(typeof(Controllers.AssemblyReference).Assembly);

builder.Services.ConfigureCors();

builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();

app.ConfigureExceptionHandler();

// app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
app.UseMiddleware<CheckUserStatusMiddleware>();
app.UseAuthorization();


app.MapControllers();

app.Run();

public partial class Program { }
