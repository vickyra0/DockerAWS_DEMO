using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Get Redis host/port from env vars
var redisHost = Environment.GetEnvironmentVariable("REDIS_HOST") ?? "localhost";
var redisPort = Environment.GetEnvironmentVariable("REDIS_PORT") ?? "6379";

var redis = ConnectionMultiplexer.Connect($"{redisHost}:{redisPort}");
var db = redis.GetDatabase();

var app = builder.Build();

app.MapGet("/", async () =>
{
    var counter = await db.StringIncrementAsync("hits");
    return $"This webpage has been viewed {counter} time(s)";
});

app.MapGet("/health", () => Results.Ok("OK"));

app.Run("http://0.0.0.0:8000");

