using Services.Contracts;

namespace Services.Implementations;

public interface IVerificationCodeService
{
    public Task<int> GenerateCodeAsync(Guid userId, CancellationToken cancellationToken);

    public Task<VerificationCodeModel?> GetCodeByUserIdAsync(Guid userId, CancellationToken cancellationToken);

}