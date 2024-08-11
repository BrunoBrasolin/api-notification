using api_notification.Contexts;
using api_notification.Models;
using api_notification.Requests;
using MediatR;

namespace api_notification.Handlers;

public class ListHandler(MainDatabaseContext context, ILogger<ListHandler> logger) : IRequestHandler<ListRequest, List<NotificationModel>>
{
	private readonly MainDatabaseContext _context = context;
	private readonly ILogger<ListHandler> _logger = logger;

	public Task<List<NotificationModel>> Handle(ListRequest request, CancellationToken cancellationToken)
	{
		_logger.LogInformation("ListHandler called");

		var allItems = _context.Notifications.ToList();

		return Task.FromResult(allItems);
	}
}
