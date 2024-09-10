using BoatSystem.Core.Entities;

public interface IOwnerRepository
{
    Task<Owner> GetByIdAsync(int id);
    Task<IEnumerable<Owner>> GetAllAsync();
    Task AddAsync(Owner owner);
    Task UpdateAsync(Owner owner);
    Task DeleteAsync(int id);
    Task<IEnumerable<Owner>> GetOwnersByUserIdAsync(string userId); // Adjusted type to string
    Task<Owner> GetByUserIdAsync(string userId);
    Task<Owner> GetOwnerByUserIdAsync(string userId); // Add this method

}
