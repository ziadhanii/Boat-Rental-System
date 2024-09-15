using BoatSystem.Core.Entities;
using BoatSystem.Core.Interfaces;
using BoatSystem.Core.Models.BoatSystem.Application.Models;
using BoatSystem.Core.Models;
using BoatSystem.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly JWT _jwt;
    private readonly ILogger<AuthService> _logger;
    private readonly IOwnerRepository _ownerRepository;
    private readonly ICustomerRepository _customerRepository;

    public AuthService(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt, ILogger<AuthService> logger,
        IOwnerRepository ownerRepository, ICustomerRepository customerRepository)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwt = jwt.Value;
        _logger = logger;
        _ownerRepository = ownerRepository;
        _customerRepository = customerRepository;
    }

    public async Task<AuthModel> Login(TokenRequestModel model)
    {
        var authModel = new AuthModel();
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
        {
            _logger.LogWarning("Login failed for email: {Email}", model.Email);
            authModel.Message = "Invalid email or password.";
            return authModel;
        }

        var jwtSecurityToken = await CreateJwtToken(user);
        var roleList = await _userManager.GetRolesAsync(user);

        authModel.IsAuthenticated = true;
        authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        authModel.Email = user.Email;
        authModel.UserName = user.UserName;
        authModel.ExpiresOn = jwtSecurityToken.ValidTo;
        authModel.Roles = roleList.ToList();
        authModel.Message = "Login successful.";

        _logger.LogInformation("User logged in successfully: {Email}", model.Email);

        return authModel;
    }

    public async Task<AuthModel> RegisterCustomerAsync(RegisterCustomerModel model)
    {
        if (await _userManager.FindByEmailAsync(model.Email) != null)
        {
            return new AuthModel { Message = "Email is already registered." };
        }

        if (await _userManager.FindByNameAsync(model.UserName) != null)
        {
            return new AuthModel { Message = "Username is already registered." };
        }

        var user = new ApplicationUser
        {
            UserName = model.UserName,
            Email = model.Email,
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            _logger.LogError("Registration failed for user: {UserName}. Errors: {Errors}", model.UserName, errors);
            return new AuthModel { Message = $"Registration failed: {errors}" };
        }

        await EnsureRoleExists("Customer");
        await _userManager.AddToRoleAsync(user, "Customer");

        var jwtSecurityToken = await CreateJwtToken(user);

        return new AuthModel
        {
            Email = user.Email,
            UserName = user.UserName,
            ExpiresOn = jwtSecurityToken.ValidTo,
            IsAuthenticated = true, 
            Roles = new List<string> { "Customer" },
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken), 
            Message = "Registration successful."
        };
    }

    public async Task<AuthModel> RegisterOwnerAsync(RegisterOwnerModel model)
    {
        if (await _userManager.FindByEmailAsync(model.Email) != null)
        {
            return new AuthModel { Message = "Email is already registered." };
        }

        if (await _userManager.FindByNameAsync(model.UserName) != null)
        {
            return new AuthModel { Message = "Username is already registered." };
        }

        var user = new ApplicationUser
        {
            UserName = model.UserName,
            Email = model.Email,
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            _logger.LogError("Registration failed for user: {UserName}. Errors: {Errors}", model.UserName, errors);
            return new AuthModel { Message = $"Registration failed: {errors}" };
        }

        await EnsureRoleExists("Owner");
        await _userManager.AddToRoleAsync(user, "Owner");

        var owner = new Owner
        {
            UserId = user.Id,
            BusinessName = model.BusinessName,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _ownerRepository.AddAsync(owner);

        return new AuthModel
        {
            Email = user.Email,
            UserName = user.UserName,
            IsAuthenticated = true,
            Roles = new List<string> { "Owner" },
            Token = null, 
            ExpiresOn = null, 
            Message = "Owner registration successful."
        };
    }

    public async Task<AuthModel> RegisterAdminAsync(RegisterAdminModel model)
    {
        if (await _userManager.FindByEmailAsync(model.Email) != null)
        {
            return new AuthModel { Message = "Email is already registered." };
        }

        if (await _userManager.FindByNameAsync(model.UserName) != null)
        {
            return new AuthModel { Message = "Username is already registered." };
        }

        var user = new ApplicationUser
        {
            UserName = model.UserName,
            Email = model.Email,
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            _logger.LogError("Registration failed for user: {UserName}. Errors: {Errors}", model.UserName, errors);
            return new AuthModel { Message = $"Registration failed: {errors}" };
        }

        await EnsureRoleExists("Admin");
        await _userManager.AddToRoleAsync(user, "Admin");

        return new AuthModel
        {
            Email = user.Email,
            UserName = user.UserName,
            IsAuthenticated = true, 
            Roles = new List<string> { "Admin" },
            Token = null, 
            ExpiresOn = null, 
            Message = "Admin registration successful."
        };
    }
    public async Task<OperationResult> VerifyOwnerAsync(string userId)
    {
        // 1. تحقق من صحة المعرف
        if (string.IsNullOrWhiteSpace(userId))
        {
            return new OperationResult { Success = false, Message = "Owner ID cannot be null or empty." };
        }

        // 2. جلب المالك بناءً على المعرف من جدول المالكين
        var owner = await _ownerRepository.GetByUserIdAsync(userId);
        if (owner == null)
        {
            return new OperationResult { Success = false, Message = "Owner not found." };
        }

        // 3. تحقق إذا كان المالك قد تم التحقق منه بالفعل
        if (owner.IsVerified)
        {
            return new OperationResult { Success = false, Message = "Owner is already verified." };
        }

        // 4. تحديث حالة التحقق
        owner.IsVerified = true;
        await _ownerRepository.UpdateAsync(owner);

        // 5. تأكيد العملية بنجاح
        return new OperationResult { Success = true, Message = "Owner verified successfully." };
    }

    public async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
    {
        var roleList = await _userManager.GetRolesAsync(user);

        var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim("UserId", user.Id) 
    }.Union(roleList.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwt.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        return new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_jwt.DurationInMinutes),
            signingCredentials: creds);
    }

    private async Task EnsureRoleExists(string roleName)
    {
        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            await _roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
    public async Task<List<Owner>> GetUnverifiedOwnersAsync()
    {
        var owners = await _ownerRepository.GetAllAsync();
        return owners.Where(o => !o.IsVerified).ToList();
    }


}
