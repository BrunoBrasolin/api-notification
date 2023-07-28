using api_notification.Models;
using MediatR;

namespace api_notification.Requests;

public class CreateRequest : IRequest<bool>
{
    public NotificationModel Notification { get; set; }
}
