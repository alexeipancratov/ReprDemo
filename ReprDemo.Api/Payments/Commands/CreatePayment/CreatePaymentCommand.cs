using ReprDemo.Api.Infrastructure;

namespace ReprDemo.Api.Payments.Commands.CreatePayment;

public class CreatePaymentCommand : ICommand<Result<Guid>>
{
    public CreatePaymentCommand(decimal amount, string from, string to, DateTimeOffset debitDate)
    {
        Amount = amount;
        From = from;
        To = to;
        DebitDate = debitDate;
    }

    public decimal Amount { get; }
    
    public string From { get; }

    public string To { get; }

    public DateTimeOffset DebitDate { get; }
}