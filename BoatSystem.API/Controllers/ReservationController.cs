using BoatSystem.Application.Queries;
using BoatSystem.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class OwnerReservationController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<OwnerReservationController> _logger;

    public OwnerReservationController(IMediator mediator, ILogger<OwnerReservationController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    [ApiExplorerSettings(GroupName = SwaggerDocsConstant.Owner)]
    //[Authorize(Roles = "owner")]
    [HttpGet("{ownerId}")]
    public async Task<IActionResult> GetOwnerReservations(int ownerId)
    {
        try
        {
            var query = new OwnerReservationsQuery { OwnerId = ownerId };
            var reservations = await _mediator.Send(query);

            if (reservations == null || !reservations.Any())
            {
                _logger.LogWarning($"No reservations found for ownerId: {ownerId}");
                return NotFound(new { Message = "No reservations found for the specified owner." });
            }

            return Ok(reservations);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while retrieving reservations for ownerId: {ownerId}");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred. Please try again later." });
        }
    }
}
