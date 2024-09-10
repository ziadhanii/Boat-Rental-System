using BoatSystem.Application.Queries.Trips;
using BoatSystem.Application.Trips.Commands;
using BoatSystem.Core.DTOs;
using BoatSystem.Core.Entities;
using BoatSystem.Core.Interfaces;
using BoatSystem.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class TripController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TripController> _logger;
    private readonly ITripService _tripService;

    public TripController(IMediator mediator, ILogger<TripController> logger, ITripService tripService)
    {
        _mediator = mediator;
        _logger = logger;
        _tripService = tripService;
    }

    // Customer endpoints
    [HttpGet("available")]
    [AllowAnonymous]
    [ApiExplorerSettings(GroupName = SwaggerDocsConstant.Customer)]
    public async Task<IActionResult> GetAvailableTrips()
    {
        try
        {
            var query = new GetAvailableTripsQuery();
            var trips = await _mediator.Send(query);

            if (!trips.Any())
            {
                return NotFound(new { Message = "No available trips found." });
            }

            return Ok(trips);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving available trips.");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred. Please try again later." });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTripById(int id)
    {
        try
        {
            var query = new GetTripByIdQuery { Id = id };
            var trip = await _mediator.Send(query);

            if (trip == null)
            {
                _logger.LogWarning($"Trip with ID {id} not found.");
                return NotFound(new { Message = "The trip you are looking for does not exist." });
            }

            return Ok(trip);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while retrieving trip with ID {id}.");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred. Please try again later." });
        }
    }

    // Owner endpoints
    [HttpPost("add")]
    [Authorize(Roles = "Owner")]
    [ApiExplorerSettings(GroupName = SwaggerDocsConstant.Owner)]
    public async Task<IActionResult> AddTrip([FromBody] CreateTripDto tripDto)
    {
        if (tripDto == null)
        {
            _logger.LogWarning("AddTrip called with null tripDto.");
            return BadRequest(new { Message = "Invalid trip data. Please provide valid trip information." });
        }

        var userId = User.FindFirst("UserId")?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            _logger.LogWarning("User ID not found in token.");
            return Unauthorized(new { Message = "User ID not found in the token. Please log in again." });
        }

        var ownerId = await _tripService.GetOwnerIdByUserIdAsync(userId);
        if (!ownerId.HasValue)
        {
            _logger.LogWarning($"Owner not found for userId: {userId}");
            return Unauthorized(new { Message = "You do not have permission to add a trip. Ensure you are logged in as an owner." });
        }

        tripDto.OwnerId = ownerId.Value;

        var command = new CreateTripCommand
        {
            BoatId = tripDto.BoatId,
            Name = tripDto.Name,
            Description = tripDto.Description,
            PricePerPerson = tripDto.PricePerPerson,
            MaxPeople = tripDto.MaxPeople,
            CancellationDeadline = tripDto.CancellationDeadline,
            OwnerId = tripDto.OwnerId,
            StartedAt = tripDto.StartedAt
        };

        try
        {
            var trip = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetTripById), new { id = trip.Id }, trip);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while adding the trip.");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred while trying to add the trip. Please try again later." });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Owner")]
    [ApiExplorerSettings(GroupName = SwaggerDocsConstant.Owner)]
    public async Task<IActionResult> UpdateTrip(int id, [FromBody] UpdateTripDto tripDto)
    {
        if (id != tripDto.Id)
        {
            return BadRequest(new { Message = "The provided ID does not match the trip ID in the request." });
        }

        var userId = User.FindFirst("UserId")?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            _logger.LogWarning("User ID not found in token.");
            return Unauthorized(new { Message = "Unable to authenticate. Please log in and try again." });
        }

        var ownerId = await _tripService.GetOwnerIdByUserIdAsync(userId);
        if (!ownerId.HasValue)
        {
            _logger.LogWarning($"Owner not found for userId: {userId}");
            return Unauthorized(new { Message = "You do not have permission to update this trip." });
        }

        var existingTrip = await _mediator.Send(new GetTripByIdQuery { Id = id });
        if (existingTrip == null)
        {
            return NotFound(new { Message = "The trip you are trying to update does not exist." });
        }

        if (existingTrip.OwnerId != ownerId.Value)
        {
            return Unauthorized(new { Message = "You do not have permission to update this trip." });
        }

        var command = new UpdateTripCommand
        {
            Id = tripDto.Id,
            BoatId = tripDto.BoatId,
            Name = tripDto.Name,
            Description = tripDto.Description,
            PricePerPerson = tripDto.PricePerPerson,
            MaxPeople = tripDto.MaxPeople,
            CancellationDeadline = tripDto.CancellationDeadline,
            Status = tripDto.Status,
            StartedAt = tripDto.StartedAt
        };

        try
        {
            var result = await _mediator.Send(command);
            if (!result)
            {
                return NotFound(new { Message = "Failed to update the trip. It may not exist or another issue occurred." });
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the trip.");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred while trying to update the trip. Please try again later." });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Owner")]
    [ApiExplorerSettings(GroupName = SwaggerDocsConstant.Owner)]
    public async Task<IActionResult> DeleteTrip(int id)
    {
        var userId = User.FindFirst("UserId")?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            _logger.LogWarning("User ID not found in token.");
            return Unauthorized(new { Message = "Unable to authenticate. Please log in and try again." });
        }

        var ownerId = await _tripService.GetOwnerIdByUserIdAsync(userId);
        if (!ownerId.HasValue)
        {
            _logger.LogWarning($"Owner not found for userId: {userId}");
            return Unauthorized(new { Message = "You do not have permission to delete this trip." });
        }

        var command = new DeleteTripCommand
        {
            Id = id,
            OwnerId = ownerId.Value
        };

        try
        {
            var result = await _mediator.Send(command);
            if (!result)
            {
                return NotFound(new { Message = "The trip you are trying to delete does not exist or could not be deleted." });
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the trip.");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred while trying to delete the trip. Please try again later." });
        }
    }

    [HttpGet("owner/{ownerId}")]
    [Authorize(Roles = "Owner")]
    [ApiExplorerSettings(GroupName = SwaggerDocsConstant.Owner)]
    public async Task<IActionResult> GetTripsByOwnerId(int ownerId)
    {
        var userId = User.FindFirst("UserId")?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            _logger.LogWarning("User ID not found in token.");
            return Unauthorized(new { Message = "Unable to authenticate. Please log in and try again." });
        }

        var ownerIdFromToken = await _tripService.GetOwnerIdByUserIdAsync(userId);
        if (!ownerIdFromToken.HasValue || ownerIdFromToken.Value != ownerId)
        {
            return Unauthorized(new { Message = "You do not have permission to view trips for this owner." });
        }

        var query = new GetTripsByOwnerIdQuery { OwnerId = ownerId };
        var trips = await _mediator.Send(query);

        if (!trips.Any())
        {
            return NotFound(new { Message = "No trips found for the specified owner." });
        }

        return Ok(trips);
    }

}
