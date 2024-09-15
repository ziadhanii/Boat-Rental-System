using BoatSystem.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BoatSystem.Core.Repositories;
using BoatSystem.Infrastructure.Data;

public class AdditionalServiceRepository : IAdditionalServiceRepository
{
    private readonly ApplicationDbContext _context;

    public AdditionalServiceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Addition>> GetAllAsync()
    {
        return await _context.Additions
            .Include(a => a.Owner) 
            .ToListAsync();
    }

    public async Task<Addition> GetByIdAsync(int id)
    {
        return await _context.Additions
            .Include(a => a.Owner) 
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Addition>> GetByOwnerIdAsync(int ownerId)
    {
        return await _context.Additions
            .Include(a => a.Owner) 
            .Where(a => a.OwnerId == ownerId)
            .ToListAsync();
    }

    public async Task<Addition> AddAsync(Addition addition)
    {
        _context.Additions.Add(addition);
        await _context.SaveChangesAsync();
        return addition;
    }

    public async Task<bool> UpdateAsync(Addition addition)
    {
        var existingAddition = await _context.Additions.FindAsync(addition.Id);
        if (existingAddition == null)
        {
            return false;
        }

        existingAddition.Name = addition.Name;
        existingAddition.Description = addition.Description;
        existingAddition.Price = addition.Price;

        _context.Additions.Update(existingAddition);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var addition = await _context.Additions.FindAsync(id);
        if (addition == null)
        {
            return false;
        }

        _context.Additions.Remove(addition);
        await _context.SaveChangesAsync();
        return true;
    }
}
