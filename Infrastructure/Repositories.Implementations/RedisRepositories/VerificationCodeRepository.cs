using Microsoft.Extensions.Options;
using Redis;
using Repositories.Abstractions;
using Services.Contracts;

namespace Repositories.Implementations.RedisRepositories;

public class VerificationCodeRepository(
    IOptions<VerificationCodeRepositoryOptions> options,
    RedisContext context) : IVerificationCodeRepository
{
    private readonly VerificationCodeRepositoryOptions _options = options.Value;
    public async Task<VerificationCodeModel?> GetCodeByUserIdAsync(Guid id, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return null;

        var codeData = await context.Database.StringGetAsync(id.ToString());

        return codeData.IsNullOrEmpty ? null : new VerificationCodeModel(id, ushort.Parse(codeData));
    }

    public async Task<bool> AddCodeAsync(VerificationCodeModel model, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return false;
        return await context.Database.StringSetAsync(
            model.Id.ToString(),
            model.VerificationCode.ToString(),
            expiry: TimeSpan.FromMinutes(_options.ExpiredTime));
    }


    public async Task<bool> DeleteCodeByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
            return false;

        return await context.Database.KeyDeleteAsync(userId.ToString());
    }
}