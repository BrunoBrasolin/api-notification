using api_notification.Models;
using api_notification.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api_notification.Controllers;

[ApiController]
[Route("")]
public class NotificationController(IMediator mediator) : Controller
{
	private readonly IMediator _mediator = mediator;

	[HttpGet]
	public async Task<List<NotificationModel>> List([FromQuery] ListRequest request) => await _mediator.Send(request);

	[HttpGet("{id}")]
	public async Task<NotificationModel> Get(int id) => await _mediator.Send(new GetRequest { Id = id });

	[HttpPost]
	public async Task<bool> Create([FromBody] CreateRequest request) => await _mediator.Send(request);

	[HttpPut("{id}")]
	public async Task<bool> Update(int id, [FromBody] UpdateRequest request)
	{
		request.Notification.Id = id;
		return await _mediator.Send(request);
	}

	[HttpDelete("{id}")]
	public async Task<bool> Delete(int id) => await _mediator.Send(new DeleteRequest { Id = id });

	[HttpGet("Dismiss")]
	public async Task<bool> Dismiss([FromQuery] DismissRequest request) => await _mediator.Send(request);

}