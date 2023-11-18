namespace ReprDemo.Api.Payments.Models;

public class CreatePaymentRequest
{
    public decimal Amount { get; set; }

    public string From { get; set; }

    public string To { get; set; }

    public DateTimeOffset DebitDate { get; set; }
}