using api_notification.Contexts;
using api_notification.Models;
using api_notification.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace api_notification.Handlers;

public class DeleteHandler : IRequestHandler<DeleteRequest, bool>
{
    private readonly IMediator _mediator;
    private readonly MainDatabaseContext _context;

    public DeleteHandler(IMediator mediator, MainDatabaseContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    public Task<bool> Handle(DeleteRequest request, CancellationToken cts)
    {
        _context.Notifications.Where(n => n.Id == request.Id).ExecuteDelete();
        _context.SaveChanges();

        GetRequest getNotification = new()
        {
            Id = request.Id
        };

        NotificationModel notification = _mediator.Send(getNotification, cts).Result;

        if (notification.Id != request.Id)
            return Task.FromResult(true);
        else
            return Task.FromResult(false);
    }
}
