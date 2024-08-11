using api_notification.Contexts;
using api_notification.Models;
using api_notification.Requests;
using MediatR;

namespace api_notification.Handlers;

public class GetHandler(MainDatabaseContext context, ILogger<GetHandler> logger) : IRequestHandler<GetRequest, NotificationModel>
{
	private readonly MainDatabaseContext _context = context;
	private readonly ILogger<GetHandler> _logger = logger;

	public Task<NotificationModel> Handle(GetRequest request, CancellationToken cancellationToken)
	{
		_logger.LogInformation("GetHandler called");

		NotificationModel notification = _context.Notifications.Where(n => n.Id == request.Id).FirstOrDefault();

		return Task.FromResult(notification);
	}
}
