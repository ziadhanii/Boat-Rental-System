using MediatR;
using BoatSystem.Core.Entities;
using System.Collections.Generic;
using BoatSystem.Core.Interfaces;

namespace BoatSystem.Application.Queries
{
    public class GetUnverifiedOwnersQuery : IRequest<List<Owner>>
    {
    }

    public class GetUnverifiedOwnersQueryHandler : IRequestHandler<GetUnverifiedOwnersQuery, List<Owner>>
    {
        private readonly IOwnerRepository _ownerRepository;

        public GetUnverifiedOwnersQueryHandler(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public async Task<List<Owner>> Handle(GetUnverifiedOwnersQuery request, CancellationToken cancellationToken)
        {
            var owners = await _ownerRepository.GetAllAsync();
            return owners.Where(o => !o.IsVerified).ToList();
        }
    }



}
