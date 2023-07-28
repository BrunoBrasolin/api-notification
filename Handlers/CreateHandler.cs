using api_notification.Contexts;
using api_notification.Models;
using api_notification.Requests;
using MediatR;

namespace api_notification.Handlers
{
    public class CreateHandler : IRequestHandler<CreateRequest, bool>
    {
        private readonly IMediator _mediator;
        private readonly MainDatabaseContext _context;

        public CreateHandler(IMediator mediator, MainDatabaseContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        public Task<bool> Handle(CreateRequest request, CancellationToken cts)
        {
            _context.Notifications.Add(request.Notification);
            _context.SaveChanges();

            GetRequest getNotification = new()
            {
                Id = request.Notification.Id
            };

            NotificationModel notification = _mediator.Send(getNotification, cts).Result;

            if (notification == request.Notification)
                return Task.FromResult(true);
            else
                return Task.FromResult(false);
        }
    }
}
