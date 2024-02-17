using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System.Net.Mime;

namespace api_notification.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthCheckController(HealthCheckService healthServiceCheck) : Controller
{
	private readonly HealthCheckService _healthServiceCheck = healthServiceCheck;

	[HttpGet]
	public async Task<IActionResult> Index()
	{
		var report = await _healthServiceCheck.CheckHealthAsync();

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

		return Content(result, MediaTypeNames.Application.Json);
	}
}