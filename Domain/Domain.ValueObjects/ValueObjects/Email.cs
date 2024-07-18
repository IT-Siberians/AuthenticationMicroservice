using Domain.ValueObjects.Validators;

namespace Domain.ValueObjects.ValueObjects;
public class Email(string value) : ValueObject<string>(new EmailValidator(), value);