using Microsoft.EntityFrameworkCore;
using GamerAPI.Models;
using GamerAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMappingService, MappingService>();

builder.Services.AddDbContext<UserContext>(opt => opt.UseInMemoryDatabase("User"));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
