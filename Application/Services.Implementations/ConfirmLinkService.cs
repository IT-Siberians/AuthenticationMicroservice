using Microsoft.Extensions.Options;
using Repositories.Abstractions;
using Services.Abstractions;
using Services.Contracts;
using static Newtonsoft.Json.JsonConvert;
using static System.Web.HttpUtility;

namespace Services.Implementations;

public class ConfirmLinkService(
    IOptions<ConfirmLinkServiceOptions> options,
    ILinkIdRepository repository) : IConfirmLinkService
{
    private readonly ConfirmLinkServiceOptions _options = options.Value;
    public async Task<Uri> GenerateConfirmEmailUriAsync(EmailConfirmationModel model,
        CancellationToken cancellationToken)
    {
        var verificationCodeModel = new VerificationCodeModel(model.Id, Guid.NewGuid());

        var token = SerializeObject(verificationCodeModel);
        var data = SerializeObject(model);

        await repository.AddVerificationCodeAsync(verificationCodeModel, cancellationToken);

        var baseUrl = "https://localhost:7263";

        // Создаем UriBuilder для формирования URL
        var uriBuilder = new UriBuilder
        {
            Scheme = "https",
            Host = "localhost",
            Port = 7263,
            Path = "api/v1/Confirms/ConfirmEmail",
            Query = $"token={Uri.EscapeDataString(token)}&data={Uri.EscapeDataString(data)}"
        };
        Console.WriteLine(uriBuilder.Uri.AbsoluteUri);
        // Возвращаем сгенерированный URL
        return uriBuilder.Uri;

        //var res = $"{_options.BaseUrl}{_options.ConfirmEmailActionPrefix}?token={Uri.EscapeDataString(token)}&data={Uri.EscapeDataString(data)}";
        //Console.WriteLine(res);
        //var result = new Uri(res, dontEscape: true);
        //Console.WriteLine(result.AbsoluteUri);
        //return result;
    }

    public Task<T?> GetDataFromLinkParameters<T>(string linkParameter)
    {
        var data = UrlDecode(linkParameter);

        return Task.FromResult<T>(DeserializeObject<T>(data));
    }
}