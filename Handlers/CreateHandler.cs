using api_notification.Contexts;
using api_notification.Models;
using api_notification.Requests;
using MediatR;

namespace api_notification.Handlers;

public class CreateHandler(IMediator mediator, MainDatabaseContext context, ILogger<CreateHandler> logger) : IRequestHandler<CreateRequest, bool>
{
	private readonly IMediator _mediator = mediator;
	private readonly MainDatabaseContext _context = context;
	private readonly ILogger<CreateHandler> _logger = logger;

	public Task<bool> Handle(CreateRequest request, CancellationToken cancellationToken)
	{
		_logger.LogInformation("CreateHandler called");
		_context.Notifications.Add(request.Notification);
		_context.SaveChanges();

		GetRequest getNotification = new()
		{
			Id = request.Notification.Id
		};

		NotificationModel notification = _mediator.Send(getNotification, cancellationToken).Result;

		if (notification == request.Notification)
		{
			_logger.LogInformation("CreateHandler success");
			return Task.FromResult(true);
		}
		else
		{
			_logger.LogError("CreateHandler error");
			return Task.FromResult(false);
		}
	}
}
