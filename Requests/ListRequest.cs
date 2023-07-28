using api_notification.Models;
using MediatR;

namespace api_notification.Requests;

public class ListRequest : IRequest<List<NotificationModel>>
{
}
