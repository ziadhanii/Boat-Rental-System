using BoatSystem.Application.Commands.Wallet;
using BoatSystem.Core.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class WalletController : ControllerBase
{
    private readonly IMediator _mediator;

    public WalletController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{ownerId}")]
    public async Task<IActionResult> GetWallet(int ownerId)
    {
        var query = new GetWalletQuery { OwnerId = ownerId };
        var wallet = await _mediator.Send(query);
        if (wallet == null) return NotFound();
        return Ok(wallet);
    }

    [HttpPost("{ownerId}/update")]
    public async Task<IActionResult> UpdateWallet(int ownerId, [FromBody] UpdateWalletDto dto)
    {
        if (ownerId != dto.OwnerId)
        {
            return BadRequest("OwnerId mismatch.");
        }

        var command = new UpdateWalletCommand { OwnerId = ownerId, Amount = dto.Amount };
        var result = await _mediator.Send(command);
        if (!result) return NotFound();
        return NoContent();
    }
}
