using api_notification.Models;
using MediatR;

namespace api_notification.Requests;

public class DeleteRequest : IRequest<bool>
{
    public int Id { get; set; }
}
