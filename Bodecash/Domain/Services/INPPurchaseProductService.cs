using BodecashAPI.Bodecash.Domain.Communication;
using BodecashAPI.Bodecash.Domain.Models;

namespace BodecashAPI.Bodecash.Domain.Services;

public interface INPPurchaseProductService
{
    Task<IEnumerable<NPPurchaseProduct>> ListAsync();
    Task<NPPurchaseProduct> GetByIdAsync(int id);
    Task<IEnumerable<NPPurchaseProduct>> GetByNPPurchaseIdAsync(int id);
    Task<NPPurchaseProductResponse> SaveAsync(NPPurchaseProduct nPPurchaseProduct);
}