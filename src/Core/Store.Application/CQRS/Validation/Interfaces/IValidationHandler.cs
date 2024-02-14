namespace Store.Application.CQRS.Validation.Interfaces;

public interface IValidationHandler
{

}

public interface IValidationHandler<T> : IValidationHandler
{

    Task<ValidationResult> Validate(T request, CancellationToken cancellationToken);

}