using Microsoft.Extensions.Options;
using Redis;
using Repositories.Abstractions;
using Services.Contracts;
using static Newtonsoft.Json.JsonConvert;

namespace Repositories.Implementations.RedisRepositories;

public class LinkIdRepository(
    IOptions<VerificationCodeRepositoryOptions> options,
    RedisContext context) : ILinkIdRepository
{
    private readonly VerificationCodeRepositoryOptions _options = options.Value;
    public async Task<VerificationCodeModel?> GetLinkIdByUserIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var codeData = await context.Database.StringGetAsync(id.ToString());

        return codeData.IsNullOrEmpty ? null : new VerificationCodeModel(id, Guid.Parse(codeData));
    }

    public async Task<bool> AddVerificationCodeAsync(VerificationCodeModel model, CancellationToken cancellationToken)
    {
        return await context.Database.StringSetAsync(model.Id.ToString(), model.LinkGuid.ToString(), expiry: TimeSpan.FromMinutes(_options.ExpiredTime));
    }
}