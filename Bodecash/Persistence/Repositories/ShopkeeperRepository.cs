using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Bodecash.Domain.Repositories;
using BodecashAPI.Bodecash.Domain.Services;
using BodecashAPI.Shared.Persistence.Contexts;
using BodecashAPI.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BodecashAPI.Bodecash.Persistence.Repositories;

public class ShopkeeperRepository : BaseRepository, IShopkeeperRepository
{
    public ShopkeeperRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Shopkeeper>> ListAsync()
    {
        return await _context.Shopkeepers.ToListAsync();
    }

    public async Task<Shopkeeper> FindByIdAsync(int id)
    {
        return await _context.Shopkeepers.FindAsync(id);
    }

    public async Task AddAsync(Shopkeeper shopkeeper)
    {
        await _context.Shopkeepers.AddAsync(shopkeeper);
    }
}