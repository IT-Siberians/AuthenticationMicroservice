using Domain.ValueObjects.Validators;

namespace Domain.ValueObjects.ValueObjects;
public class Username(string value) : ValueObject<string>(new UsernameValidator(), value);