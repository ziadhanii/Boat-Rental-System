using BoatSystem.Application.Queries.Cstomer;
using BoatSystem.Core.DTOs;
using BoatSystem.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
[ApiExplorerSettings(GroupName = SwaggerDocsConstant.Customer)]
//[Authorize(Roles = "Customer")]
public class CustomerWalletController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<CustomerWalletController> _logger;

    public CustomerWalletController(IMediator mediator, ILogger<CustomerWalletController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetWallet(int customerId)
    {
        try
        {
            var query = new CustomerWalletQuery { CustomerId = customerId };
            var wallet = await _mediator.Send(query);

            if (wallet == null)
            {
                _logger.LogWarning($"Wallet not found for customerId: {customerId}");
                return NotFound(new { Message = "Wallet not found for the specified customer." });
            }

            return Ok(wallet);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while retrieving wallet for customerId: {customerId}");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred. Please try again later." });
        }
    }

    [HttpPost("{customerId}/update")]
    public async Task<IActionResult> UpdateWallet(int customerId, [FromBody] UpdateCustomerWalletDto dto)
    {
        if (customerId != dto.CustomerId)
        {
            _logger.LogWarning($"CustomerId mismatch: Provided {dto.CustomerId}, expected {customerId}");
            return BadRequest(new { Message = "CustomerId mismatch." });
        }

        try
        {
            var command = new UpdateCustomerWalletCommand { CustomerId = customerId, Amount = dto.Amount };
            var result = await _mediator.Send(command);

            if (!result)
            {
                _logger.LogWarning($"Failed to update wallet for customerId: {customerId}");
                return NotFound(new { Message = "Failed to update wallet. The wallet may not exist." });
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while updating wallet for customerId: {customerId}");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred while trying to update the wallet. Please try again later." });
        }
    }
}
