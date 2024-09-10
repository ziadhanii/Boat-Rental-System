using BoatSystem.Core.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatSystem.Application.Commands.Wallet
{

        public class GetWalletQuery : IRequest<WalletDto>
        {
            public int OwnerId { get; set; }
        }

    public class UpdateWalletCommand : IRequest<bool>
    {
        public int OwnerId { get; set; }
        public decimal Amount { get; set; }
    }

    public class GetWalletQueryHandler : IRequestHandler<GetWalletQuery, WalletDto>
    {
        private readonly IOwnerRepository _ownerRepository;

        public GetWalletQueryHandler(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public async Task<WalletDto> Handle(GetWalletQuery request, CancellationToken cancellationToken)
        {
            var owner = await _ownerRepository.GetByIdAsync(request.OwnerId);
            if (owner == null)
            {
                return null; // أو رمي استثناء مناسب
            }

            return new WalletDto
            {
                OwnerId = owner.Id,
                Balance = owner.WalletBalance
            };
        }
    }
    public class UpdateWalletCommandHandler : IRequestHandler<UpdateWalletCommand, bool>
    {
        private readonly IOwnerRepository _ownerRepository;

        public UpdateWalletCommandHandler(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public async Task<bool> Handle(UpdateWalletCommand request, CancellationToken cancellationToken)
        {
            var owner = await _ownerRepository.GetByIdAsync(request.OwnerId);
            if (owner == null) return false;

            owner.WalletBalance += request.Amount;
            await _ownerRepository.UpdateAsync(owner);

            return true;
        }
    }

}
