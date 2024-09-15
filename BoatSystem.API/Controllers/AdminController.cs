using BoatSystem.Application.Commands.BoatCommands;
using BoatSystem.Application.Commands.ReservationCommands;
using BoatSystem.Application.Commands.UserCommands;
using BoatSystem.Application.DTOs;
using BoatSystem.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BoatSystem.Application;
using BoatSystem.Core.Models;

namespace BoatSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = SwaggerDocsConstant.Admin)]
    //[Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [HttpPost("approve-user/{userId}")]
        public async Task<IActionResult> ApproveUser(string userId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId == userId)
            {
                return Forbid("Admin cannot approve or reject their own account.");
            }

            try
            {
                await _mediator.Send(new ApproveUserRegistrationCommand { UserId = userId });
                return Ok(new { message = "User has been approved successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while approving the user.", details = ex.Message });
            }
        }

        [HttpPost("reject-user/{userId}")]
        public async Task<IActionResult> RejectUser(string userId)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId == userId)
            {
                return Forbid("Admin cannot reject their own account.");
            }

            try
            {
                await _mediator.Send(new RejectUserRegistrationCommand { UserId = userId });
                return Ok(new { message = "User has been rejected successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while rejecting the user.", details = ex.Message });
            }
        }

        [HttpPost("approve-boat/{boatId}")]
        public async Task<IActionResult> ApproveBoat(int boatId)
        {
            try
            {
                await _mediator.Send(new ApproveBoatRegistrationCommand { BoatId = boatId });
                return Ok(new { message = "Boat has been approved successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while approving the boat.", details = ex.Message });
            }
        }

        [HttpPost("reject-boat/{boatId}")]
        public async Task<IActionResult> RejectBoat(int boatId)
        {
            try
            {
                await _mediator.Send(new RejectBoatRegistrationCommand { BoatId = boatId });
                return Ok(new { message = "Boat has been rejected successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while rejecting the boat.", details = ex.Message });
            }
        }

        [HttpGet("monitor-reservations")]
        public async Task<IActionResult> MonitorReservations()
        {
            try
            {
                var reservations = await _mediator.Send(new MonitorReservationsQuery());
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving reservations.", details = ex.Message });
            }
        }

        [HttpPost("cancel-reservation/{id}")]
        public async Task<IActionResult> CancelReservation(int id)
        {
            try
            {
                await _mediator.Send(new CancelReservationCommand { ReservationId = id });
                return NoContent();
            }
            catch (Exception ex)
            {
                var responseMessage = ex.Message.Contains("already cancelled")
                    ? "The reservation has already been canceled. Please check the reservation status or contact support if you need further assistance."
                    : "Oops! It looks like there was an issue while processing your request. Please try again later or contact support if the problem persists.";

                return StatusCode(500, new
                {
                    message = responseMessage,
                    details = ex.Message
                });
            }
        }

        [HttpPut("update-reservation/{id}")]
        public async Task<IActionResult> UpdateReservation(int id, [FromBody] UpdateReservationDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new { message = "Reservation data is required." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid reservation data.", details = ModelState });
            }

            try
            {
                await _mediator.Send(new UpdateReservationCommand
                {
                    ReservationId = id,
                    ReservationDto = dto
                });
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpPost("verify-owner/{userId}")]
        public async Task<IActionResult> VerifyOwner(string userId)
        {
            try
            {
                var result = await _mediator.Send(new VerifyOwnerCommand { UserId = userId });
                if (!result.IsSuccess)
                {
                    return BadRequest(new { Message = result.Message });
                }
                return Ok(new { Message = result.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while verifying the owner.", Details = ex.Message });
            }
        }

        [HttpGet("unverified")]
        public async Task<IActionResult> GetUnverifiedOwners()
        {
            try
            {
                var owners = await _mediator.Send(new GetUnverifiedOwnersQuery());
                if (owners == null || !owners.Any())
                {
                    return NotFound(new { Message = "No unverified owners found." });
                }
                return Ok(owners);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while retrieving unverified owners.", Details = ex.Message });
            }
        }
    }
}
