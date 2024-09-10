//using BoatSystem.Core.DTOs;
//using BoatSystem.Core.Entities;
//using BoatSystem.Core.Interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using System.Linq;
//using BoatSystem.Application.Services;

//[Authorize(Roles = "Customer")]
//[ApiController]
//[Route("api/[controller]")]
//public class BookingController : ControllerBase
//{
//    private readonly IBookingService _bookingService;
//    private readonly ILogger<BookingController> _logger;

//    public BookingController(IBookingService bookingService, ILogger<BookingController> logger)
//    {
//        _bookingService = bookingService;
//        _logger = logger;
//    }

//    [HttpPost("book")]
//    public async Task<IActionResult> BookTrip([FromBody] BoatBookingDto bookingDto)
//    {
//        if (bookingDto == null)
//        {
//            _logger.LogWarning("BookTrip called with null bookingDto.");
//            return BadRequest(new { Message = "Please provide valid booking data." });
//        }

//        var userId = User.FindFirst("UserId")?.Value;
//        if (string.IsNullOrEmpty(userId))
//        {
//            _logger.LogWarning("User ID not found in token.");
//            return Unauthorized("User ID not found in token.");
//        }
//        var customerId = await _bookingService.GetCustomerIdByUserIdAsync(userId);
//        if (customerId == null)
//        {
//            _logger.LogWarning($"Customer ID not found for User ID: {userId}");
//            return Unauthorized(new { Message = "Unable to find customer associated with this User ID." });
//        }

//        if (!await _bookingService.IsTripExistsAsync(bookingDto.TripId))
//        {
//            _logger.LogWarning($"Trip with ID {bookingDto.TripId} not found.");
//            return BadRequest(new { Message = "The specified trip does not exist." });
//        }

//        var booking = new BoatBooking
//        {
//            CustomerId = customerId.Value,
//            BoatId = bookingDto.BoatId,
//            TripId = bookingDto.TripId,
//            BookingDate = DateTime.UtcNow,
//            DurationHours = bookingDto.DurationHours,
//            TotalPrice = bookingDto.TotalPrice,
//            Status = "Pending",
//            BookingAdditions = bookingDto.AdditionalServices.Select(a => new BookingAddition
//            {
//                AdditionId = a.AdditionId,
//                Quantity = a.Quantity,
//                TotalPrice = a.TotalPrice
//            }).ToList()
//        };

//        try
//        {
//            await _bookingService.CreateBookingAsync(booking);
//            return CreatedAtAction(nameof(GetBookingById), new { id = booking.Id }, booking);
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError(ex, "An error occurred while booking the trip.");
//            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred while trying to book the trip. Please try again later." });
//        }
//    }

//    [HttpGet("{id}")]
//    public async Task<IActionResult> GetBookingById(int id)
//    {
//        try
//        {
//            var booking = await _bookingService.GetBookingByIdAsync(id);

//            if (booking == null)
//            {
//                _logger.LogWarning($"Booking with ID {id} not found.");
//                return NotFound(new { Message = "The booking you are looking for does not exist." });
//            }

//            return Ok(booking);
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError(ex, $"An error occurred while retrieving booking with ID {id}.");
//            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred. Please try again later." });
//        }
//    }

//    [HttpGet("customer/{userId}")]
//    public async Task<IActionResult> GetCustomerIdByUserIdAsync(string userId)
//    {
//        var customerId = await _bookingService.GetCustomerIdByUserIdAsync(userId);
//        if (customerId == null)
//        {
//            return NotFound(new { Message = "Customer ID not found." });
//        }

//        return Ok(new { CustomerId = customerId });
//    }
//}
