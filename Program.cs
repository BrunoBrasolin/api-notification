using api_notification.Contexts;
using Microsoft.EntityFrameworkCore;
using Gamidas.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(option => option.RegisterServicesFromAssemblyContaining(typeof(Program)));
builder.Services.AddDbContext<MainDatabaseContext>(option => option.UseOracle(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddHealthChecks().AddOracle(builder.Configuration.GetConnectionString("Default"));
builder.Services.AddCors();
builder.Services.ConfigureGamidas();
builder.Configuration.SetBasePath("/app/config");
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.Run();
