using api_notification.Requests;
using Gamidas.Utils.Exceptions;
using MediatR;

namespace api_notification.Handlers;

public class DismissHandler : IRequestHandler<DismissRequest, bool>
{
    private readonly IMediator _mediator;

    public DismissHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task<bool> Handle(DismissRequest request, CancellationToken cts)
    {
        ValidationRequest validationRequest = new()
        {
            APIKey = request.APIKey
        };

        bool validated = _mediator.Send(validationRequest, cts).Result;

        if (!validated)
            throw new InvalidAPIKeyException();

        DeleteRequest getNotification = new()
        {
            Id = request.Id
        };

        bool deleted = _mediator.Send(getNotification, cts).Result;

        return Task.FromResult(deleted);
    }
}
