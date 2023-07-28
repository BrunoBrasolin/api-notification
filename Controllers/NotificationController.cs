using api_notification.Models;
using api_notification.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_notification.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : Controller
{
    private readonly IMediator _mediator;

    public NotificationController(IMediator mediator) => _mediator = mediator;

    [HttpGet("ListNotifications")]
    public async Task<List<NotificationModel>> ListNotifications([FromQuery] ListRequest request) => await _mediator.Send(request);

    [HttpGet("GetNotification")]
    public async Task<NotificationModel> GetNotification([FromQuery] GetRequest request) => await _mediator.Send(request);

    [HttpPost("CreateNotification")]
    public async Task<bool> CreateNotification([FromBody] CreateRequest request) => await _mediator.Send(request);

    [HttpPut("UpdateNotification")]
    public async Task<bool> UpdateNotification([FromBody] UpdateRequest request) => await _mediator.Send(request);

    [HttpDelete("DeleteNotification")]
    public async Task<bool> DeleteNotification([FromBody] DeleteRequest request) => await _mediator.Send(request);
}