using Microsoft.AspNetCore.Mvc;
using BoatSystem.Core.Interfaces;
using System.Threading.Tasks;
using BoatSystem.Application.Commands.ReservationCommands;
using MediatR;
using BoatSystem.Core.Models; // تأكد من إضافة هذا الاستيراد

namespace BoatSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = SwaggerDocsConstant.Admin)]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IMediator _mediator; // إضافة _mediator هنا

        public AdminController(IAdminService adminService, IMediator mediator) // إضافة mediator إلى البناء
        {
            _adminService = adminService;
            _mediator = mediator; // تعيين mediator
        }

        [HttpPost("approve-user/{userId}")]
        public async Task<IActionResult> ApproveUser(string userId)
        {
            await _adminService.ApproveUserRegistrationAsync(userId);
            return Ok();
        }

        [HttpPost("reject-user/{userId}")]
        public async Task<IActionResult> RejectUser(string userId)
        {
            await _adminService.RejectUserRegistrationAsync(userId);
            return Ok();
        }

        [HttpPost("approve-boat/{boatId}")]
        public async Task<IActionResult> ApproveBoat(int boatId)
        {
            await _adminService.ApproveBoatRegistrationAsync(boatId);
            return Ok();
        }

        [HttpPost("reject-boat/{boatId}")]
        public async Task<IActionResult> RejectBoat(int boatId)
        {
            await _adminService.RejectBoatRegistrationAsync(boatId);
            return Ok();
        }


        [HttpGet("monitor-reservations")]
        public async Task<IActionResult> MonitorReservations()
        {
            var reservations = await _adminService.MonitorReservationsAsync();
            return Ok(reservations);
        }


        [HttpPost("cancel-reservation/{id}")]
        public async Task<IActionResult> CancelReservation(int id)
        {
            await _mediator.Send(new CancelReservationCommand { ReservationId = id });
            return NoContent();
        }

        [HttpPost("update-reservation/{id}")]
        public async Task<IActionResult> UpdateReservation(int id, [FromBody] UpdateReservationCommand command)
        {
            command.ReservationId = id;
            await _mediator.Send(command);
            return NoContent();
        }

        //[HttpGet("monitor-reservations")]
        //public async Task<IActionResult> MonitorReservations()
        //{
        //    var reservations = await _mediator.Send(new GetAllReservationsQuery()); // هنا يمكنك تنفيذ استعلام لجلب جميع الحجوزات
        //    return Ok(reservations);
        //}
    }
}
