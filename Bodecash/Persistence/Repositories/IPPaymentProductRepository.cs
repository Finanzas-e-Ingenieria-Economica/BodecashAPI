using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Bodecash.Domain.Repositories;
using BodecashAPI.Shared.Persistence.Contexts;
using BodecashAPI.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BodecashAPI.Bodecash.Persistence.Repositories;

public class IPPaymentProductRepository : BaseRepository, IIPPaymentProductRepository
{
    public IPPaymentProductRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<IPPaymentProduct>> ListAsync()
    {
        return await _context.IPPaymentProducts.ToListAsync();
    }

    public async Task<IPPaymentProduct> FindByIdAsync(int id)
    {
        return await _context.IPPaymentProducts.FindAsync(id);
    }

    public async Task<IEnumerable<IPPaymentProduct>> GetByIPPaymentIdAsync(int id)
    {
        return await _context.IPPaymentProducts.Where(x => x.IPPaymentId == id).ToListAsync();
    }

    public async Task AddAsync(IPPaymentProduct IPPaymentProduct)
    {
        await _context.IPPaymentProducts.AddAsync(IPPaymentProduct);
    }
}