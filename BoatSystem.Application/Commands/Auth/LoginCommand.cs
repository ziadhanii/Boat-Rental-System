using BoatSystem.Core.Entities;
using BoatSystem.Core.Interfaces;
using BoatSystem.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BoatSystem.Application
{
    public class LoginCommand : IRequest<AuthModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthModel>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthService _authService;
        private readonly IOwnerRepository _ownerRepository;

        public LoginCommandHandler(UserManager<ApplicationUser> userManager, IAuthService authService, IOwnerRepository ownerRepository)
        {
            _userManager = userManager;
            _authService = authService;
            _ownerRepository = ownerRepository;
        }

        public async Task<AuthModel> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var authModel = new AuthModel();
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                authModel.Message = "Email or password is incorrect";
                return authModel;
            }

            var owner = await _ownerRepository.GetByUserIdAsync(user.Id);
            if (user != null && owner != null && !owner.IsVerified)
            {
                authModel.Message = "Owner account is not verified";
                return authModel;
            }

            var jwtToken = await _authService.CreateJwtToken(user);
            var roleList = await _userManager.GetRolesAsync(user);

            authModel.IsAuthenticated = true;
            authModel.ExpiresOn = jwtToken.ValidTo;
            authModel.Email = user.Email;
            authModel.UserName = user.UserName;
            authModel.Roles = roleList.ToList();
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return authModel;
        }
    }
}
