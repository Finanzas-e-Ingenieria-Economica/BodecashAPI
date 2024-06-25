using BodecashAPI.Bodecash.Domain.Models;

namespace BodecashAPI.Bodecash.Domain.Repositories;

public interface INPPurchaseRepository
{
    Task<IEnumerable<NPPurchase>> ListAsync();
    Task<NPPurchase> FindByIdAsync(int id);
    Task<IEnumerable<NPPurchase>> ListByNormalPurchaseIdAsync(int normalPurchaseId);
    Task AddAsync(NPPurchase nPPurchase);
    void Update(NPPurchase nPPurchase);
}