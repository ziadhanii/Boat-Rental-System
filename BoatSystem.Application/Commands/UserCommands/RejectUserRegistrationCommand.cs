﻿using BoatSystem.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BoatSystem.Application.Commands.UserCommands
{
    public class RejectUserRegistrationCommand : IRequest
    {
        public string UserId { get; set; }
    }

    public class RejectUserRegistrationCommandHandler : IRequestHandler<RejectUserRegistrationCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOwnerRepository _ownerRepository;

        public RejectUserRegistrationCommandHandler(IUserRepository userRepository, IOwnerRepository ownerRepository)
        {
            _userRepository = userRepository;
            _ownerRepository = ownerRepository;
        }

        public async Task Handle(RejectUserRegistrationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user != null)
            {
                user.IsApproved = false;
                await _userRepository.UpdateAsync(user);

                var owner = await _ownerRepository.GetByUserIdAsync(request.UserId);
                if (owner != null)
                {
                    owner.IsApproved = false;
                    await _ownerRepository.UpdateAsync(owner);
                }
            }
        }
    }
}
