using BoatSystem.Application.Commands.BoatCommands;
using BoatSystem.Application.DTOs;
using BoatSystem.Core.DTOs;
using BoatSystem.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BoatSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoatController : ControllerBase
    {
        private readonly IBoatService _boatService;
        private readonly IMediator _mediator;

        public BoatController(IBoatService boatService, IMediator mediator)
        {
            _boatService = boatService;
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBoat([FromBody] BoatDto boatDto)
        {
            if (boatDto == null)
            {
                return BadRequest("Invalid boat data.");
            }

            int boatId = await _boatService.AddBoatAsync(boatDto);

            if (boatId > 0)
            {
                return CreatedAtAction(nameof(GetBoatById), new { id = boatId }, boatDto);
            }

            return StatusCode(500, "An error occurred while adding the boat.");
        }

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

        [HttpGet("searchByName")]
        public async Task<IActionResult> GetBoatsByName([FromQuery] string name)
        {
            var boats = await _boatService.GetBoatsByNameAsync(name);
            return Ok(boats);
        }

        [HttpGet("unapproved")]
        public async Task<IActionResult> GetUnapprovedBoats()
        {
            var boats = await _boatService.GetUnapprovedBoatsAsync();
            return Ok(boats);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBoat(int id, [FromBody] UpdateBoatCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("ID mismatch. The provided ID does not match the ID in the command.");
            }

            var result = await _mediator.Send(command);
            if (result)
            {
                return NoContent();
            }

            return NotFound(); // إرجاع NotFound إذا لم يتم العثور على القارب
        }
    }
}
