using BodecashAPI.Bodecash.Domain.Communication;
using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Bodecash.Resources.Get;

namespace BodecashAPI.Bodecash.Domain.Services;

public interface INPPurchaseService
{
    Task<IEnumerable<NPPurchase>> ListAsync();
    Task<NPPurchase> GetByIdAsync(int id);
    Task<IEnumerable<NPPurchase>> ListByNormalPurchaseIdAsync(int normalPurchaseId);
    Task<NPPurchaseResponse> SaveAsync(NPPurchase nPPurchase);
}