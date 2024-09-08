using BoatSystem.Core.Interfaces;
using BoatSystem.Core.Models.BoatSystem.Application.Models;
using BoatSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;
using BoatSystem.Infrastructure.Repositories;
using BoatSystem.Application;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] TokenRequestModel model)
    {
        var authModel = await _authService.Login(model);
        if (!authModel.IsAuthenticated)
        {
            return Unauthorized(new { Message = authModel.Message });
        }
        return Ok(authModel);
    }

    [HttpPost("register-customer")]
    public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerModel model)
    {
        var authModel = await _authService.RegisterCustomerAsync(model);
        if (!authModel.IsAuthenticated && !string.IsNullOrEmpty(authModel.Message))
        {
            return BadRequest(new { Message = authModel.Message });
        }
        return Ok(authModel); // Ensure the complete `AuthModel` is returned
    }

    [HttpPost("register-owner")]
    public async Task<IActionResult> RegisterOwner([FromBody] RegisterOwnerModel model)
    {
        var authModel = await _authService.RegisterOwnerAsync(model);
        if (!authModel.IsAuthenticated)
        {
            return BadRequest(new { Message = authModel.Message });
        }
        return Ok(authModel);
    }

    [HttpPost("register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterAdminModel model)
    {
        var authModel = await _authService.RegisterAdminAsync(model);
        if (!authModel.IsAuthenticated)
        {
            return BadRequest(new { Message = authModel.Message });
        }
        return Ok(authModel);
    }

}
