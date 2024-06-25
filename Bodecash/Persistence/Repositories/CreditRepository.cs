using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Bodecash.Domain.Repositories;
using BodecashAPI.Shared.Persistence.Contexts;
using BodecashAPI.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BodecashAPI.Bodecash.Persistence.Repositories;

public class CreditRepository : BaseRepository,ICreditRepository
{
    public CreditRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Credit>> ListAsync()
    {
        return await _context.Credits.ToListAsync();
    }

    public async Task<Credit> FindByIdAsync(int id)
    {
        return await _context.Credits.FindAsync(id);
    }

    public async Task<IEnumerable<Credit>> ListByClientIdAsync(int clientId)
    {
        return await _context.Credits.Where(x => x.ClientId == clientId).ToListAsync();
    }

    public async Task<IEnumerable<Credit>> ListByShopkeeperIdAsync(int shopkeeperId)
    {
        return await _context.Credits.Where(x => x.ShopkeeperId == shopkeeperId).ToListAsync();
    }

    public async Task<Credit> FindUnpaidByClientId(int id)
    {
        return await _context.Credits.Where(x=>x.ClientId == id && x.IsCreditPayed == false).FirstOrDefaultAsync();
    }

    public async Task AddAsync(Credit credit)
    {
        await _context.Credits.AddAsync(credit);
    }

    public void Update(Credit credit)
    {
        _context.Update(credit);
    }
}