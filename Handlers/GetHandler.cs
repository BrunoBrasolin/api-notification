using api_notification.Contexts;
using api_notification.Models;
using api_notification.Requests;
using MediatR;

namespace api_notification.Handlers;

public class GetHandler : IRequestHandler<GetRequest, NotificationModel>
{
    private readonly MainDatabaseContext _context;

    public GetHandler(MainDatabaseContext context)
    {
        _context = context;
    }

    public Task<NotificationModel> Handle(GetRequest request, CancellationToken cts)
    {
        NotificationModel notification = _context.Notifications.Where(n => n.Id == request.Id).FirstOrDefault();

        return Task.FromResult(notification);
    }
}
