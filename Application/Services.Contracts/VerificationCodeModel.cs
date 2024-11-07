namespace Services.Contracts;

public record VerificationCodeModel(Guid Id, Guid LinkGuid) : BaseModel<Guid>(Id);