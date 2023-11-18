using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReprDemo.Api.Infrastructure;
using ReprDemo.Api.Payments.Commands.CreatePayment;
using ReprDemo.Api.Payments.Models;

namespace ReprDemo.Api.Payments.Endpoints;

public sealed class CreatePaymentEndpoint : EndpointBaseAsync
    .WithRequest<CreatePaymentRequest>
    .WithResult<IActionResult>
{
    private readonly ISender _sender;

    public CreatePaymentEndpoint(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost(PaymentRoutes.Create)]
    public override async Task<IActionResult> HandleAsync(
        CreatePaymentRequest request,
        CancellationToken cancellationToken = default) =>
        await Result.Create(request, null)
            .Map(req => new CreatePaymentCommand(req.Amount, req.From, req.To, req.DebitDate))
            .Bind(command => _sender.Send(command, cancellationToken))
            .Match(Okay, HandleFailure);

    private IActionResult Okay(Guid result)
    {
        return Ok(result);
    }
    
    private IActionResult HandleFailure(Error arg)
    {
        if (arg.Code == "validation.negativeAmount")
        {
            return BadRequest(arg.Message);
        }

        return StatusCode(500, arg.Message);
    }
}
