using BoatSystem.Application.Commands.Wallet;
using BoatSystem.Core.DTOs;
using BoatSystem.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

[Route("api/[controller]")]
[ApiController]
public class WalletController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<WalletController> _logger;

    public WalletController(IMediator mediator, ILogger<WalletController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [ApiExplorerSettings(GroupName = SwaggerDocsConstant.Owner)]
    //[Authorize(Roles = "Owner")]
    [HttpGet("{ownerId}")]
    public async Task<IActionResult> GetWallet(int ownerId)
    {
        try
        {
            var query = new GetWalletQuery { OwnerId = ownerId };
            var wallet = await _mediator.Send(query);

            if (wallet == null)
            {
                _logger.LogWarning($"Wallet not found for ownerId: {ownerId}");
                return NotFound(new { Message = "Wallet not found." });
            }

            return Ok(wallet);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving the wallet.");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred. Please try again later." });
        }
    }
    [ApiExplorerSettings(GroupName = SwaggerDocsConstant.Owner)]
    //[Authorize(Roles = "owner")]
    [HttpPost("{ownerId}/update")]
    public async Task<IActionResult> UpdateWallet(int ownerId, [FromBody] UpdateWalletDto dto)
    {
        if (dto == null)
        {
            _logger.LogWarning("UpdateWallet called with null dto.");
            return BadRequest(new { Message = "Invalid wallet data. Please provide valid information." });
        }

        if (ownerId != dto.OwnerId)
        {
            _logger.LogWarning($"OwnerId mismatch. Expected: {ownerId}, Provided: {dto.OwnerId}");
            return BadRequest(new { Message = "OwnerId mismatch." });
        }

        try
        {
            var command = new UpdateWalletCommand { OwnerId = ownerId, Amount = dto.Amount };
            var result = await _mediator.Send(command);

            if (!result)
            {
                _logger.LogWarning($"Failed to update wallet for ownerId: {ownerId}");
                return NotFound(new { Message = "Failed to update wallet. The wallet may not exist." });
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the wallet.");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred. Please try again later." });
        }
    }
}
