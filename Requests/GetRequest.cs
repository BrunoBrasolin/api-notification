using api_notification.Models;
using MediatR;

namespace api_notification.Requests;

public class GetRequest : IRequest<NotificationModel>
{
    public int Id { get; set; }
}
