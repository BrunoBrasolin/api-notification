using api_notification.Contexts;
using api_notification.Models;
using api_notification.Requests;
using MediatR;

namespace api_notification.Handlers;

public class UpdateHandler(IMediator mediator, MainDatabaseContext context, ILogger<UpdateHandler> logger) : IRequestHandler<UpdateRequest, bool>
{
	private readonly IMediator _mediator = mediator;
	private readonly MainDatabaseContext _context = context;
	private readonly ILogger<UpdateHandler> _logger = logger;

	public Task<bool> Handle(UpdateRequest request, CancellationToken cancellationToken)
	{
		_logger.LogInformation("UpdateHandler called");

		NotificationModel updatedNotification = _context.Notifications.Update(request.Notification).Entity;
		_context.SaveChanges();

		GetRequest getNotification = new()
		{
			Id = updatedNotification.Id.Value
		};

		NotificationModel notification = _mediator.Send(getNotification, cancellationToken).Result;

		if (notification == request.Notification)
		{
			_logger.LogInformation("UpdateHandler success");
			return Task.FromResult(true);
		}
		else
		{
			_logger.LogError("UpdateHandler error");
			return Task.FromResult(false);
		}
	}
}
