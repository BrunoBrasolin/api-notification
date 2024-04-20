using api_notification.Contexts;
using api_notification.Models;
using api_notification.Requests;
using MediatR;

namespace api_notification.Handlers;

public class ListHandler : IRequestHandler<ListRequest, List<NotificationModel>>
{
    private readonly MainDatabaseContext _context;

    public ListHandler(MainDatabaseContext context)
    {
        _context = context;
    }

    public Task<List<NotificationModel>> Handle(ListRequest request, CancellationToken cancellationToken)
    {
        var allItems = _context.Notifications.ToList();

        return Task.FromResult(allItems);
    }
}
