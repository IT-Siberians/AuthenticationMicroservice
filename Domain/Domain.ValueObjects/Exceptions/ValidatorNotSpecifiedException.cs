namespace Domain.ValueObjects.Exceptions;

public class ValidatorNotSpecifiedException(object valueObject)
    : Exception($"Validator must be specified for type {valueObject.GetType()}");