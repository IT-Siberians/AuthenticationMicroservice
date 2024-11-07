using Services.Contracts;

namespace Services.Abstractions;

public interface IConfirmLinkService
{
    public Task<Uri> GenerateConfirmEmailUriAsync(EmailConfirmationModel model, CancellationToken cancellationToken);
    public Task<T?> GetDataFromLinkParameters<T>(string linkParameter);
}