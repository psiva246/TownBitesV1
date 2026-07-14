using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TownBites.Infrastructure.Data;
var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// API Explorer & Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TownBites API",
        Version = "v1",
        Description = "Restaurant Ordering API"
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();