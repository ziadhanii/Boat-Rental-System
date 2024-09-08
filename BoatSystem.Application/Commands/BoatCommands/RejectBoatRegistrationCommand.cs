using BoatSystem.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BoatSystem.Application.Commands.BoatCommands
{
    public class RejectBoatRegistrationCommand : IRequest<Unit>
    {
        public int BoatId { get; set; }
    }

    public class RejectBoatRegistrationCommandHandler : IRequestHandler<RejectBoatRegistrationCommand, Unit>
    {
        private readonly IBoatRepository _boatRepository;

        public RejectBoatRegistrationCommandHandler(IBoatRepository boatRepository)
        {
            _boatRepository = boatRepository;
        }

        public async Task<Unit> Handle(RejectBoatRegistrationCommand request, CancellationToken cancellationToken)
        {
            var boat = await _boatRepository.GetByIdAsync(request.BoatId);
            if (boat != null)
            {
                boat.IsApproved = false; // تأكد من أن الخاصية موجودة في الكائن Boat
                await _boatRepository.UpdateAsync(boat); // تأكد من تنفيذ عملية التحديث بشكل صحيح
            }
            return Unit.Value;
        }
    }
}
