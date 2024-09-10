using BoatSystem.Application.Queries.Cstomer;
using BoatSystem.Core.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class CustomerWalletController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerWalletController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetWallet(int customerId)
    {
        var query = new CustomerWalletQuery { CustomerId = customerId };
        var wallet = await _mediator.Send(query);
        if (wallet == null) return NotFound();
        return Ok(wallet);
    }

    [HttpPost("{customerId}/update")]
    public async Task<IActionResult> UpdateWallet(int customerId, [FromBody] UpdateCustomerWalletDto dto)
    {
        if (customerId != dto.CustomerId)
        {
            return BadRequest("CustomerId mismatch.");
        }

        var command = new UpdateCustomerWalletCommand { CustomerId = customerId, Amount = dto.Amount };
        var result = await _mediator.Send(command);
        if (!result) return NotFound();
        return NoContent();
    }
}
