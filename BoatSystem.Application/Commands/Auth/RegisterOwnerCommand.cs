using BoatSystem.Core.Entities;
using BoatSystem.Core.Interfaces;
using BoatSystem.Core.Models;
using BoatSystem.Core.Models.BoatSystem.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BoatSystem.Application.Commands.Auth
{
    public class RegisterOwnerCommand : ICommand<AuthModel>
    {
        public RegisterCustomerModel Model { get; set; }
        public RegisterOwnerCommand(RegisterCustomerModel model)
        {
            Model = model;
        }
    }

    public class RegisterOwnerCommandHandler : ICommandHandler<RegisterOwnerCommand, AuthModel>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOwnerRepository _ownerRepository;

        public RegisterOwnerCommandHandler(UserManager<ApplicationUser> userManager, IOwnerRepository ownerRepository)
        {
            _userManager = userManager;
            _ownerRepository = ownerRepository;
        }

        public async Task<AuthModel> Handle(RegisterOwnerCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                return new AuthModel { Message = "Email is already registered" };
            }

            if (await _userManager.FindByNameAsync(model.UserName) != null)
            {
                return new AuthModel { Message = "UserName is already registered" };
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return new AuthModel { Message = string.Join(", ", result.Errors.Select(e => e.Description)) };
            }

            await _userManager.AddToRoleAsync(user, "Owner");
            var owner = new Owner
            {
                UserId = user.Id,
                IsVerified = false,
            };
            await _ownerRepository.AddAsync(owner);
            return new AuthModel { Message = "Owner registered successfully, please verify account" };
        }
    }
}
