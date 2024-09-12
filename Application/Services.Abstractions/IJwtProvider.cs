using Domain.Entities;
using Domain.Entities.Domain.Enums;
using Services.Contracts;

namespace Services.Abstractions;

public interface IJwtProvider
{
    public string GenerateTokenString(UserModel? user, TokenType tokenType);
}