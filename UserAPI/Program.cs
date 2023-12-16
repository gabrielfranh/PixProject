using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserAPI.Context;
using UserAPI.Mapping;
using UserAPI.Repositories;
using UserAPI.Repositories.Interfaces;
using UserAPI.Services;
using UserAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
Console.WriteLine($"Environment: {environment}");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserContext>(options => 
        options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"), 
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySqlConnection"))));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
