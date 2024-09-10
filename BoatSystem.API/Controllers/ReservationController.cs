using BoatSystem.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class OwnerReservationController : ControllerBase
{
    private readonly IMediator _mediator;

    public OwnerReservationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{ownerId}")]
    public async Task<IActionResult> GetOwnerReservations(int ownerId)
    {
        var query = new OwnerReservationsQuery { OwnerId = ownerId };
        var reservations = await _mediator.Send(query);
        if (reservations == null || !reservations.Any()) return NotFound();
        return Ok(reservations);
    }
}
