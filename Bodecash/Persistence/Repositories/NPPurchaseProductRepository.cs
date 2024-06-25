using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Bodecash.Domain.Repositories;
using BodecashAPI.Shared.Persistence.Contexts;
using BodecashAPI.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BodecashAPI.Bodecash.Persistence.Repositories;

public class NPPurchaseProductRepository : BaseRepository, INPPurchaseProductRepository
{
    public NPPurchaseProductRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<NPPurchaseProduct>> ListAsync()
    {
        return await _context.NPPurchaseProducts.ToListAsync();
    }

    public async Task<NPPurchaseProduct> FindByIdAsync(int id)
    {
        return await _context.NPPurchaseProducts.FindAsync(id);
    }

    public async Task<IEnumerable<NPPurchaseProduct>> GetByNPPurchaseIdAsync(int id)
    {
        return await _context.NPPurchaseProducts.Where(x => x.NPPurchaseId == id).ToListAsync();
    }

    public async Task AddAsync(NPPurchaseProduct nPPurchaseProduct)
    {
        await _context.NPPurchaseProducts.AddAsync(nPPurchaseProduct);
    }
}