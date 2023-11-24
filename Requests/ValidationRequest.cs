using MediatR;

namespace api_notification.Requests;

public class ValidationRequest : IRequest<bool>
{
    public string APIKey { get; set; }
}
