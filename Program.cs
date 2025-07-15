using api_notification.Contexts;
using Microsoft.EntityFrameworkCore;
using Gamidas.Utils;
using Serilog;
using Serilog.Events;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System.Net.Mime;

var builder = WebApplication.CreateBuilder(args);
if (builder.Environment.EnvironmentName == "Production")
	builder.Configuration.SetBasePath("/app/config");
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(option => option.RegisterServicesFromAssemblyContaining(typeof(Program)));
string connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<MainDatabaseContext>(option => option.UseOracle(connectionString));
builder.Services.AddHealthChecks().AddOracle(connectionString);
builder.Services.AddCors();
builder.Services.ConfigureGamidas();
builder.Services.AddSerilog();

Log.Logger = new LoggerConfiguration()
	.Enrich.WithProperty("ApplicationName", "API-Notification")
	.MinimumLevel.Warning()
	.MinimumLevel.Override("api_notification", LogEventLevel.Information)
	.WriteTo.Oracle(cfg => cfg.WithSettings(connectionString)
		.UseBurstBatch()
		.CreateSink())
	.CreateLogger();

var app = builder.Build();
app.UseSwagger(c => { c.RouteTemplate = "notification/{documentName}/swagger.json"; });
app.UseSwaggerUI(c =>
{
	c.SwaggerEndpoint("/notification/v1/swagger.json", "API Notification V1");
	c.RoutePrefix = "notification";
}); app.UseAuthorization();
app.MapControllers();

app.MapHealthChecks("/healthcheck", new HealthCheckOptions
{
	ResponseWriter = async (context, report) =>
	{
		string appHealth = Enum.GetName(typeof(HealthStatus), HealthStatus.Healthy);

		var dependencies = report.Entries.Select(e =>
		{

			HealthStatus status = e.Value.Status;

			if (status != HealthStatus.Healthy)
				appHealth = Enum.GetName(typeof(HealthStatus), HealthStatus.Unhealthy);

			return new
			{
				key = e.Key,
				value = Enum.GetName(typeof(HealthStatus), e.Value.Status)
			};
		}).ToList();

		var result = JsonConvert.SerializeObject(new
		{
			status = appHealth,
			dependencies
		});
		context.Response.ContentType = MediaTypeNames.Application.Json;
		await context.Response.WriteAsync(result);
	}
});

app.Run();
