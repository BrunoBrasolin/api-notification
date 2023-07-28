using api_notification.Models;
using MediatR;

namespace api_notification.Requests;

public class UpdateRequest : IRequest<bool>
{
    public NotificationModel Notification { get; set; }
}
