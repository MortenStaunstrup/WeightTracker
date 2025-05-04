using DotNetEnv;
using WeightTrackerAPI.Repositories;
using WeightTrackerAPI.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

builder.Services.AddSingleton<IUserRepository, UserPostGresRepository>();
builder.Services.AddSingleton<IWeightRepository, WeightPostGresRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("policy",
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/", () => $"Connectionstring: {connectionString}");

app.UseHttpsRedirection();

app.UseCors("policy");

app.UseAuthorization();

app.MapControllers();

app.Run();