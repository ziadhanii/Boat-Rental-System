using BoatSystem.Core.Interfaces;
using BoatSystem.Core.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using BoatSystem.Application.Commands.Wallet;
using BoatSystem.Core.Exceptions;
using BoatSystem.Application.Queries;
using BoatSystem.Application.Commands;
using BoatSystem.Application.Services;
using BoatSystem.Core.Models;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICostCalculatorService _costCalculator;
    private readonly IBoatService _boatService;
    private readonly IBookingService _bookingService;
    private readonly ILogger<BookingsController> _logger;

    public BookingsController(
        IMediator mediator,
        ICostCalculatorService costCalculator,
        IBoatService boatService,
        IBookingService bookingService,
        ILogger<BookingsController> logger)
    {
        _mediator = mediator;
        _costCalculator = costCalculator;
        _boatService = boatService;
        _bookingService = bookingService;
        _logger = logger;
    }
    [ApiExplorerSettings(GroupName = SwaggerDocsConstant.Customer)]
    //[Authorize(Roles = "Customer")]
    [HttpPost("calculate-total-cost")]
    public async Task<IActionResult> CalculateTotalCost([FromBody] TotalCostRequestDto request)
    {
        if (request == null)
        {
            _logger.LogWarning("Cost calculation request received with null data.");
            return BadRequest("Invalid request data.");
        }

        try
        {
            var totalPrice = await _costCalculator.CalculateTotalCostAsync(request.TripId, request.NumberOfPeople, request.AdditionalServiceIds);
            var response = new TotalCostResponseDto { TotalPrice = totalPrice };
            return Ok(response);
        }
        catch (NotFoundException ex)
        {
            _logger.LogError(ex, "Error occurred while calculating total cost.");
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while calculating total cost.");
            return StatusCode(500, "Internal server error");
        }
    }
    [AllowAnonymous]
    [HttpGet("available-boats")]
    public async Task<IActionResult> GetAvailableBoats()
    {
        try
        {
            var boats = await _boatService.GetAvailableBoatsAsync();
            return Ok(boats);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while fetching available boats.");
            return StatusCode(500, "Internal server error");
        }
    }

    [ApiExplorerSettings(GroupName = SwaggerDocsConstant.Customer)]
    //[Authorize(Roles = "Customer")]
    [HttpGet("history/{customerId}")]
    public async Task<ActionResult<BookingHistoryResponse>> GetBookingHistory(int customerId)
    {
        var query = new GetBookingHistoryQuery(customerId);
        var response = await _mediator.Send(query);
        return Ok(response);
    }
    [ApiExplorerSettings(GroupName = SwaggerDocsConstant.Customer)]
    //[Authorize(Roles = "Customer")]
    [HttpPost("cancel")]
    public async Task<IActionResult> CancelBooking([FromBody] CancelBookingCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    [ApiExplorerSettings(GroupName = SwaggerDocsConstant.Customer)]
    //[Authorize(Roles = "Customer")]
    [HttpPost("book-boat")]
    public async Task<IActionResult> BookBoat([FromBody] BoatBookingRequest request)
    {
        if (request == null)
        {
            _logger.LogWarning("Booking request received with null data.");
            return BadRequest("Invalid booking data.");
        }

        try
        {
            var result = await _bookingService.BookBoatAsync(request);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        catch (NotFoundException ex)
        {
            _logger.LogError(ex, "Error occurred while processing the booking request.");
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the booking request.");
            return StatusCode(500, "Internal server error");
        }
    }
    [ApiExplorerSettings(GroupName = SwaggerDocsConstant.Customer)]
    //[Authorize(Roles = "Customer")]
    [HttpPost("book-trip")]
    public async Task<IActionResult> BookTrip([FromBody] TripBookingRequest request)
    {
        if (request == null)
        {
            _logger.LogWarning("Booking request received with null data.");
            return BadRequest("Invalid booking data.");
        }

        try
        {
            var result = await _bookingService.BookTripAsync(request);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        catch (NotFoundException ex)
        {
            _logger.LogError(ex, "Error occurred while processing the booking request.");
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the booking request.");
            return StatusCode(500, "Internal server error");
        }
    }
}
