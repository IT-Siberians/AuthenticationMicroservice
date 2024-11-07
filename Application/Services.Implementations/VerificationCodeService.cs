using Repositories.Abstractions;
using Services.Contracts;

namespace Services.Implementations;

    public class VerificationCodeService(
        IVerificationCodeRepository repository): IVerificationCodeService
    {
        public async Task<int> GenerateCodeAsync(Guid userId, CancellationToken cancellationToken)
        {
            var code = new Random().Next(100000, 999999);

            await repository.AddVerificationCodeAsync(new VerificationCodeModel(userId, code), cancellationToken);

            return code;
        }

        public async Task<VerificationCodeModel?> GetCodeByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await repository.GetVerificationCodeByUserIdAsync(userId, cancellationToken);
        }
    }