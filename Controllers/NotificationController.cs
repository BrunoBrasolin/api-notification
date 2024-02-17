using api_notification.Models;
using api_notification.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api_notification.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController(IMediator mediator) : Controller
{
    private readonly IMediator _mediator = mediator;

	[HttpGet("List")]
    public async Task<List<NotificationModel>> List([FromQuery] ListRequest request) => await _mediator.Send(request);

    [HttpGet("Get")]
    public async Task<NotificationModel> Get([FromQuery] GetRequest request) => await _mediator.Send(request);

    [HttpPost("Create")]
    public async Task<bool> Create([FromBody] CreateRequest request) => await _mediator.Send(request);

    [HttpPut("Update")]
    public async Task<bool> Update([FromBody] UpdateRequest request) => await _mediator.Send(request);

    [HttpDelete("Delete")]
    public async Task<bool> Delete([FromBody] DeleteRequest request) => await _mediator.Send(request);

    [HttpGet("Dismiss")]
    public async Task<bool> Dismiss([FromQuery] DismissRequest request) => await _mediator.Send(request);

}