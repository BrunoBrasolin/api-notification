using api_notification.Requests;
using Gamidas.Utils.Exceptions;
using MediatR;

namespace api_notification.Handlers;

public class DismissHandler(IMediator mediator) : IRequestHandler<DismissRequest, bool>
{
	private readonly IMediator _mediator = mediator;

	public Task<bool> Handle(DismissRequest request, CancellationToken cancellationToken)
	{
		ValidationRequest validationRequest = new()
		{
			APIKey = request.APIKey
		};

		bool validated = _mediator.Send(validationRequest, cancellationToken).Result;

		if (!validated)
			throw new InvalidAPIKeyException();

		DeleteRequest getNotification = new()
		{
			Id = request.Id
		};

		bool deleted = _mediator.Send(getNotification, cancellationToken).Result;

		return Task.FromResult(deleted);
	}
}
