using BoatSystem.Core.Entities;
using BoatSystem.Core.Interfaces;
using BoatSystem.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoatSystem.Application.Services
{
    public class AdditionalService : IAdditionalService
    {
        private readonly IAdditionalServiceRepository _additionalRepository;

        public AdditionalService(IAdditionalServiceRepository additionalRepository)
        {
            _additionalRepository = additionalRepository;
        }

        public async Task<Addition> GetByIdAsync(int id)
        {
            return await _additionalRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Addition>> GetByOwnerIdAsync(int ownerId)
        {
            return await _additionalRepository.GetByOwnerIdAsync(ownerId);
        }



        public async Task<Addition> AddAsync(Addition addition)
        {
            return await _additionalRepository.AddAsync(addition);
        }

        public async Task<bool> UpdateAsync(Addition addition)
        {
            return await _additionalRepository.UpdateAsync(addition);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _additionalRepository.DeleteAsync(id);
        }
    }
}
