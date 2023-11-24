using api_notification.Contexts;
using api_notification.Models;
using api_notification.Requests;
using Gamidas.Utils.Encryption;
using MediatR;

namespace api_notification.Handlers;

public class ValidationHandler : IRequestHandler<ValidationRequest, bool>
{
    private readonly IConfiguration _configuration;
    private readonly IEncryption _encryption;

    public ValidationHandler(IConfiguration configuration, IEncryption encryption)
    {
        _configuration = configuration;
        _encryption = encryption;
    }

    public Task<bool> Handle(ValidationRequest request, CancellationToken cts)
    {
        string apiKey = _configuration.GetValue<string>("APIKey");

        bool compared = _encryption.CompareEncryptedData(request.APIKey, apiKey);

        return Task.FromResult(compared);
    }
}
