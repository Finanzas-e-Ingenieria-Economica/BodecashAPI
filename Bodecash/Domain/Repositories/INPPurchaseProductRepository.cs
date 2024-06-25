using BodecashAPI.Bodecash.Domain.Models;

namespace BodecashAPI.Bodecash.Domain.Repositories;

public interface INPPurchaseProductRepository
{
    Task<IEnumerable<NPPurchaseProduct>> ListAsync();
    Task<NPPurchaseProduct> FindByIdAsync(int id);
    Task<IEnumerable<NPPurchaseProduct>> GetByNPPurchaseIdAsync(int id);
    Task AddAsync(NPPurchaseProduct nPPurchaseProduct);
}