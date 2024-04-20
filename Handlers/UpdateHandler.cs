using api_notification.Contexts;
using api_notification.Models;
using api_notification.Requests;
using MediatR;

namespace api_notification.Handlers;

public class UpdateHandler : IRequestHandler<UpdateRequest, bool>
{
    private readonly IMediator _mediator;
    private readonly MainDatabaseContext _context;

    public UpdateHandler(IMediator mediator, MainDatabaseContext context)
    {
        _mediator = mediator;
        _context = context;
    }
    public Task<bool> Handle(UpdateRequest request, CancellationToken cancellationToken)
    {
        NotificationModel updatedNotification = _context.Notifications.Update(request.Notification).Entity;
        _context.SaveChanges();

        GetRequest getNotification = new()
        {
            Id = updatedNotification.Id
        };

        NotificationModel notification = _mediator.Send(getNotification, cancellationToken).Result;

        if (notification == request.Notification)
            return Task.FromResult(true);
        else
            return Task.FromResult(false);
    }
}
