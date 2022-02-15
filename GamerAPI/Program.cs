using GamerAPI.Models;
using GamerAPI.Services;
using Microsoft.EntityFrameworkCore;
using Polly;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMappingService, MappingService>();

builder.Services.AddHttpClient<IGameService, GameService>(client =>
{
    client.BaseAddress = new Uri(configuration["RAWG:BaseUrl"]);
})
    .AddTransientHttpErrorPolicy(policy =>
        policy.WaitAndRetryAsync(2, _ => TimeSpan.FromSeconds(2)))
    .AddTransientHttpErrorPolicy(policy =>
        policy.CircuitBreakerAsync(2, TimeSpan.FromSeconds(5)));

builder.Services.AddDbContext<UserContext>(opt => opt.UseInMemoryDatabase("User"));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
