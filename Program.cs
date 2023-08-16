using api_notification.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(option => option.RegisterServicesFromAssemblyContaining(typeof(Program)));
builder.Services.AddDbContext<MainDatabaseContext>(option => option.UseOracle(builder.Configuration.GetConnectionString("MainDatabase")));


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
