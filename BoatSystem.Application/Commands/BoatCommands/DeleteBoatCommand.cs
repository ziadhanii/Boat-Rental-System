using BoatSystem.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BoatSystem.Application.Commands.BoatCommands
{
    public class DeleteBoatCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public int OwnerId { get; set; } // Ensure this is an int
    }

    public class DeleteBoatCommandHandler : IRequestHandler<DeleteBoatCommand, bool>
    {
        private readonly IBoatRepository _boatRepository;
        private readonly IOwnerRepository _ownerRepository;

        public DeleteBoatCommandHandler(IBoatRepository boatRepository, IOwnerRepository ownerRepository)
        {
            _boatRepository = boatRepository;
            _ownerRepository = ownerRepository;
        }

        public async Task<bool> Handle(DeleteBoatCommand request, CancellationToken cancellationToken)
        {
            var boat = await _boatRepository.GetByIdAsync(request.Id);
            if (boat == null)
            {
                return false; // Boat not found
            }

            var owner = await _ownerRepository.GetByIdAsync(request.OwnerId);
            if (owner == null)
            {
                return false; // Owner not found
            }

            if (boat.OwnerId != request.OwnerId)
            {
                return false; // Owner mismatch
            }

            await _boatRepository.DeleteAsync(request.Id);
            return true;
        }
    }
}
