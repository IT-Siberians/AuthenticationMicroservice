namespace Services.Contracts;

public record VerificationCodeModel(Guid Id, int VerificationCode) : BaseModel<Guid>(Id);