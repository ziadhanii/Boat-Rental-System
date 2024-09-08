using BoatSystem.Core.Interfaces;
using BoatSystem.Core.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BoatSystem.Application
{
    public class VerifyOwnerCommand : IRequest<Result>
    {
        public string UserId { get; set; }
    }

    public class VerifyOwnerCommandHandler : IRequestHandler<VerifyOwnerCommand, Result>
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IUserRepository _userRepository;

        public VerifyOwnerCommandHandler(IOwnerRepository ownerRepository, IUserRepository userRepository)
        {
            _ownerRepository = ownerRepository;
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(VerifyOwnerCommand request, CancellationToken cancellationToken)
        {
            // التحقق من صحة معرف المستخدم
            if (string.IsNullOrWhiteSpace(request.UserId))
            {
                return Result.Failure("UserId cannot be null or empty");
            }

            // استرجاع المستخدم بناءً على userId
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                return Result.Failure("User not found");
            }

            // استرجاع المالك بناءً على userId
            var owner = await _ownerRepository.GetByUserIdAsync(request.UserId);
            if (owner == null)
            {
                return Result.Failure("Owner not found");
            }

            // تحديث حالة التحقق
            if (owner.IsVerified)
            {
                return Result.Failure("Owner is already verified");
            }
            owner.IsVerified = true;
            await _ownerRepository.UpdateAsync(owner);

            return Result.Success("Owner verified successfully");
        }
    }
}
