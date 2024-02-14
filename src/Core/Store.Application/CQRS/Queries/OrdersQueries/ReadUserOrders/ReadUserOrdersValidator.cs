using Store.Application.CQRS.Validation;
using Store.Application.CQRS.Validation.Interfaces;
using Store.Application.Interfaces.Repositories;

namespace Store.Application.CQRS.Queries.OrdersQueries.ReadUserOrders;

internal class ReadUserOrdersValidator : IValidationHandler<ReadUserOrdersQuery>
{

    private readonly IUserRepository _userRepository;

    public ReadUserOrdersValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ValidationResult> Validate(ReadUserOrdersQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.ReadAsync(request.UserId, cancellationToken) == null
            ? ValidationResult.Fail($"A user with this id - {request.UserId} does not exist")
            : ValidationResult.Success;
    }

}