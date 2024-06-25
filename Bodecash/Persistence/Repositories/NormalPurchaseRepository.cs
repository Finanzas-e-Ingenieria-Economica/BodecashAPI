using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Bodecash.Domain.Repositories;
using BodecashAPI.Shared.Persistence.Contexts;
using BodecashAPI.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BodecashAPI.Bodecash.Persistence.Repositories;

public class NormalPurchaseRepository : BaseRepository, INormalPurchaseRepository
{
    public NormalPurchaseRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<NormalPurchase>> ListAsync()
    {
        return await _context.NormalPurchases.ToListAsync();
    }

    public async Task<NormalPurchase> FindByIdAsync(int id)
    {
        return await _context.NormalPurchases.FindAsync(id);
    }

    public async Task<IEnumerable<NormalPurchase>> ListByCreditIdAsync(int creditId)
    {
        return await _context.NormalPurchases.Where(x => x.CreditId == creditId).ToListAsync();
    }

    public async Task AddAsync(NormalPurchase normalPurchase)
    {
        await _context.NormalPurchases.AddAsync(normalPurchase);
    }
}