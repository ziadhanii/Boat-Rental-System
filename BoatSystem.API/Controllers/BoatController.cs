using BoatSystem.Application.Commands.BoatCommands;
using BoatSystem.Core.DTOs;
using BoatSystem.Core.Entities;
using BoatSystem.Core.Interfaces;
using BoatSystem.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
//[Authorize(Roles = "Owner")]
[ApiExplorerSettings(GroupName = SwaggerDocsConstant.Owner)]
public class BoatController : ControllerBase
{
    private readonly IBoatService _boatService;
    private readonly IMediator _mediator;
    private readonly ILogger<BoatController> _logger;

    public BoatController(IBoatService boatService, IMediator mediator, ILogger<BoatController> logger)
    {
        _boatService = boatService;
        _mediator = mediator;
        _logger = logger;
    }
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBoatById(int id)
    {
        var boat = await _boatService.GetBoatByIdAsync(id);
        if (boat == null)
        {
            return NotFound();
        }
        return Ok(boat);
    }

    [ApiExplorerSettings(GroupName = SwaggerDocsConstant.Owner)]
    //[Authorize(Roles = "owner")]
    [HttpPost("add")]
    public async Task<IActionResult> AddBoat([FromBody] BoatDto boatDto)
    {
        if (boatDto == null)
        {
            _logger.LogWarning("AddBoat called with null boatDto.");
            return BadRequest("Invalid boat data.");
        }

        var userId = User.FindFirst("UserId")?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            _logger.LogWarning("User ID not found in token.");
            return Unauthorized("User ID not found in token.");
        }

        var ownerId = await _boatService.GetOwnerIdByUserIdAsync(userId);
        if (!ownerId.HasValue)
        {
            _logger.LogWarning($"Owner not found for userId: {userId}");
            return Unauthorized("Owner not found.");
        }

        boatDto.OwnerId = ownerId.Value;

        try
        {
            var boatDetails = await _boatService.AddBoatAsync(boatDto);
            if (boatDetails != null)
            {
                return CreatedAtAction(nameof(GetBoatById), new { id = boatDetails.Id }, boatDetails);
            }

            _logger.LogError("Error occurred while adding the boat.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the boat.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred while adding the boat.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the boat.");
        }
    }
    [ApiExplorerSettings(GroupName = SwaggerDocsConstant.Owner)]
    //[Authorize(Roles = "owner")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBoat(int id, [FromBody] UpdateBoatCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("ID mismatch. The provided ID does not match the ID in the command.");
        }

        var userId = User.FindFirst("UserId")?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            _logger.LogWarning("User ID not found in token.");
            return Unauthorized("User ID not found in token.");
        }

        var ownerId = await _boatService.GetOwnerIdByUserIdAsync(userId);
        if (!ownerId.HasValue)
        {
            _logger.LogWarning($"Owner not found for userId: {userId}");
            return Unauthorized("Owner not found.");
        }

        var boat = await _boatService.GetBoatByIdAsync(id);
        if (boat == null || boat.OwnerId != ownerId.Value)
        {
            _logger.LogWarning($"Boat with ID {id} does not belong to owner with ID {ownerId.Value}.");
            return Unauthorized("You are not authorized to update this boat.");
        }

        var result = await _mediator.Send(command);
        if (result)
        {
            return NoContent();
        }

        return NotFound();
    }
    [ApiExplorerSettings(GroupName = SwaggerDocsConstant.Owner)]
    //[Authorize(Roles = "owner")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBoat(int id)
    {
        var userId = User.FindFirst("UserId")?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            _logger.LogWarning("User ID not found in token.");
            return Unauthorized("User ID not found in token.");
        }

        var ownerId = await _boatService.GetOwnerIdByUserIdAsync(userId);
        if (!ownerId.HasValue)
        {
            _logger.LogWarning($"Owner not found for userId: {userId}");
            return Unauthorized("Owner not found.");
        }

        var boat = await _boatService.GetBoatByIdAsync(id);
        if (boat == null)
        {
            return NotFound("Boat not found.");
        }

        if (boat.OwnerId != ownerId.Value)
        {
            _logger.LogWarning($"Boat with ID {id} does not belong to owner with ID {ownerId.Value}.");
            return Unauthorized("You are not authorized to delete this boat.");
        }

        try
        {
            var command = new DeleteBoatCommand
            {
                Id = id,
                OwnerId = ownerId.Value
            };

            var result = await _mediator.Send(command);
            if (result)
            {
                return NoContent();
            }

            return StatusCode(StatusCodes.Status400BadRequest, "Cannot delete the boat due to existing reservations or owner mismatch.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred while deleting the boat.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the boat.");
        }
    }
}
