using ReprDemo.Api.Infrastructure;

namespace ReprDemo.Api.Payments.Commands.CreatePayment;

public class CreatePaymentCommandHandler : ICommandHandler<CreatePaymentCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        if (request.Amount < 0)
        {
            return Result.Failure<Guid>(new Error("validation.negativeAmount", "Amount cannot be negative."));
        }

        if (request.Amount > 100)
        {
            return Result.Failure<Guid>(new Error("validation.internalError", "An internal error occurred."));
        }

        return Result.Success(Guid.NewGuid());
    }
}