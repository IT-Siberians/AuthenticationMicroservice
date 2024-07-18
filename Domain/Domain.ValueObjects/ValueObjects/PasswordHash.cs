using Domain.ValueObjects.Validators;

namespace Domain.ValueObjects.ValueObjects;
public class PasswordHash(string value) : ValueObject<string>(new PasswordHashValidator(), value);