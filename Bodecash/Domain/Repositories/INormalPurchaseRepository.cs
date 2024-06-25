using BodecashAPI.Bodecash.Domain.Models;

namespace BodecashAPI.Bodecash.Domain.Repositories;

public interface INormalPurchaseRepository
{
    Task<IEnumerable<NormalPurchase>> ListAsync();
    Task<NormalPurchase> FindByIdAsync(int id);
    Task<IEnumerable<NormalPurchase>> ListByCreditIdAsync(int creditId);
    Task AddAsync(NormalPurchase normalPurchase);
}