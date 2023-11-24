using MediatR;

namespace api_notification.Requests;

public class DismissRequest : IRequest<bool>
{
    public int Id { get; set; }
    public string APIKey { get; set; }
}
