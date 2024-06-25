using BodecashAPI.Bodecash.Domain.Communication;
using BodecashAPI.Bodecash.Domain.Models;

namespace BodecashAPI.Bodecash.Domain.Services;

public interface INormalPurchaseService
{
    Task<IEnumerable<NormalPurchase>> ListAsync();
    Task<NormalPurchase> GetByIdAsync(int id);
    Task<IEnumerable<NormalPurchase>> ListByCreditIdAsync(int creditId);
    Task<NormalPurchaseResponse> SaveAsync(NormalPurchase normalPurchase, Credit credit);
}