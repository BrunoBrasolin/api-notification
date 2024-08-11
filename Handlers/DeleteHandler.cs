using api_notification.Contexts;
using api_notification.Models;
using api_notification.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace api_notification.Handlers;

public class DeleteHandler(IMediator mediator, MainDatabaseContext context, ILogger<DeleteHandler> logger) : IRequestHandler<DeleteRequest, bool>
{
	private readonly IMediator _mediator = mediator;
	private readonly MainDatabaseContext _context = context;
	private readonly ILogger<DeleteHandler> _logger = logger;

	public Task<bool> Handle(DeleteRequest request, CancellationToken cancellationToken)
	{
		_logger.LogInformation("DeleteHandler called");

		_context.Notifications.Where(n => n.Id == request.Id).ExecuteDelete();
		_context.SaveChanges();

		GetRequest getNotification = new()
		{
			Id = request.Id
		};

		NotificationModel notification = _mediator.Send(getNotification, cancellationToken).Result;

		if (notification == null)
		{
			_logger.LogInformation("DeleteHandler success");
			return Task.FromResult(true);
		}
		else
		{
			_logger.LogError("DeleteHandler error");
			return Task.FromResult(false);
		}
	}
}
