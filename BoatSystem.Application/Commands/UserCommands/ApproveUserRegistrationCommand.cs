using BoatSystem.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

public class ApproveUserRegistrationCommand : IRequest
{
    public string UserId { get; set; }
}

public class ApproveUserRegistrationCommandHandler : IRequestHandler<ApproveUserRegistrationCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IOwnerRepository _ownerRepository; // إضافة هذا

    public ApproveUserRegistrationCommandHandler(IUserRepository userRepository, IOwnerRepository ownerRepository)
    {
        _userRepository = userRepository;
        _ownerRepository = ownerRepository;
    }

    public async Task Handle(ApproveUserRegistrationCommand request, CancellationToken cancellationToken)
    {
        // تحديث المستخدم في جدول AspNetUsers
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user != null)
        {
            user.IsApproved = true;
            await _userRepository.UpdateAsync(user);

            // تحديث المالك في جدول Owners
            var owner = await _ownerRepository.GetByUserIdAsync(request.UserId); // تأكد من أن لديك هذا الأسلوب
            if (owner != null)
            {
                owner.IsApproved = true;
                await _ownerRepository.UpdateAsync(owner);
            }
        }
    }
}
