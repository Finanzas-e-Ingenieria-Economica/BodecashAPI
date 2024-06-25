using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Bodecash.Domain.Repositories;
using BodecashAPI.Shared.Persistence.Contexts;
using BodecashAPI.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BodecashAPI.Bodecash.Persistence.Repositories;

public class NPPurchaseRepository : BaseRepository, INPPurchaseRepository
{
    public NPPurchaseRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<NPPurchase>> ListAsync()
    {
        return await _context.NPPurchases.ToListAsync();
    }

    public async Task<NPPurchase> FindByIdAsync(int id)
    {
        return await _context.NPPurchases.FindAsync(id);
    }

    public async Task<IEnumerable<NPPurchase>> ListByNormalPurchaseIdAsync(int normalPurchaseId)
    {
        return await _context.NPPurchases.Where(x=>x.NormalPurchaseId == normalPurchaseId).ToListAsync();
    }

    public async Task AddAsync(NPPurchase nPPurchase)
    {
        await _context.NPPurchases.AddAsync(nPPurchase);
    }

    public void Update(NPPurchase nPPurchase)
    {
        _context.Update(nPPurchase);
    }
}