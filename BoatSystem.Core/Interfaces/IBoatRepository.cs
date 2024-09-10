using BoatSystem.Core.DTOs;
using BoatSystem.Core.Entities;

public interface IBoatRepository
{
    Task<Boat> GetByIdAsync(int id);
    Task<IEnumerable<Boat>> GetAllAsync();
    Task AddAsync(Boat boat);
    Task UpdateAsync(Boat boat);
    Task DeleteAsync(int id);
    Task<IEnumerable<Boat>> GetBoatsByOwnerIdAsync(int ownerId);
    Task<IEnumerable<Boat>> GetBoatsByNameAsync(string name);
    Task<IEnumerable<Boat>> GetUnapprovedBoatsAsync();
    Task<IEnumerable<BoatDto>> GetAvailableBoatsAsync(); // تأكد من وجود هذه الطريقة

}
