using BoatSystem.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BoatSystem.Application.Commands.BoatCommands
{
    // تعريف الأمر UpdateBoatCommand
    public class UpdateBoatCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public decimal ReservationPrice { get; set; }
        public int OwnerId { get; set; }
    }

    // تعريف المعالج UpdateBoatCommandHandler
    public class UpdateBoatCommandHandler : IRequestHandler<UpdateBoatCommand, bool>
    {
        private readonly IBoatRepository _boatRepository;

        public UpdateBoatCommandHandler(IBoatRepository boatRepository)
        {
            _boatRepository = boatRepository;
        }

        public async Task<bool> Handle(UpdateBoatCommand request, CancellationToken cancellationToken)
        {
            var boat = await _boatRepository.GetByIdAsync(request.Id);
            if (boat == null)
            {
                return false; // قارب غير موجود
            }

            // تحديث الخصائص
            boat.Name = request.Name;
            boat.Description = request.Description;
            boat.Capacity = request.Capacity;
            boat.ReservationPrice = request.ReservationPrice;
            boat.OwnerId = request.OwnerId;
            boat.UpdatedAt = DateTime.UtcNow;

            await _boatRepository.UpdateAsync(boat);
            return true; // التحديث ناجح
        }
    }
}
