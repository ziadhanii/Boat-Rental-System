using BoatSystem.Core.Interfaces;
using BoatSystem.Core.Models.BoatSystem.Application.Models;
using BoatSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] TokenRequestModel model)
    {
        _logger.LogInformation("Login attempt with email: {Email}", model.Email);

        var authModel = await _authService.Login(model);
        if (!authModel.IsAuthenticated)
        {
            _logger.LogWarning("Login failed for email: {Email}. Reason: {Message}", model.Email, authModel.Message);
            return Unauthorized(new { Message = authModel.Message });
        }

        _logger.LogInformation("Login successful for email: {Email}", model.Email);
        return Ok(authModel);
    }

    [ApiExplorerSettings(GroupName = SwaggerDocsConstant.Customer)]
    [HttpPost("register-customer")]
    public async Task<IActionResult> RegisterCustomer([FromBody] RegisterCustomerModel model)
    {
        _logger.LogInformation("Registering customer with email: {Email}", model.Email);

        var authModel = await _authService.RegisterCustomerAsync(model);
        if (!authModel.IsAuthenticated)
        {
            _logger.LogWarning("Customer registration failed for email: {Email}. Reason: {Message}", model.Email, authModel.Message);
            return BadRequest(new { Message = authModel.Message });
        }

        _logger.LogInformation("Customer registration successful for email: {Email}", model.Email);
        return Ok(authModel);
    }

    [ApiExplorerSettings(GroupName = SwaggerDocsConstant.Owner)]
    [HttpPost("register-owner")]
    public async Task<IActionResult> RegisterOwner([FromBody] RegisterOwnerModel model)
    {
        _logger.LogInformation("Registering owner with email: {Email}", model.Email);

        var authModel = await _authService.RegisterOwnerAsync(model);
        if (!authModel.IsAuthenticated)
        {
            _logger.LogWarning("Owner registration failed for email: {Email}. Reason: {Message}", model.Email, authModel.Message);
            return BadRequest(new { Message = authModel.Message });
        }

        _logger.LogInformation("Owner registration successful for email: {Email}", model.Email);
        return Ok(authModel);
    }

    [ApiExplorerSettings(GroupName = SwaggerDocsConstant.Admin)]
    [HttpPost("register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterAdminModel model)
    {
        _logger.LogInformation("Registering admin with email: {Email}", model.Email);

        var authModel = await _authService.RegisterAdminAsync(model);
        if (!authModel.IsAuthenticated)
        {
            _logger.LogWarning("Admin registration failed for email: {Email}. Reason: {Message}", model.Email, authModel.Message);
            return BadRequest(new { Message = authModel.Message });
        }

        _logger.LogInformation("Admin registration successful for email: {Email}", model.Email);
        return Ok(authModel);
    }
}
