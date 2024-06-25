using BodecashAPI.Bodecash.Domain.Communication;
using BodecashAPI.Bodecash.Domain.Models;

namespace BodecashAPI.Bodecash.Domain.Services;

public interface ICreditService
{
    Task<IEnumerable<Credit>> ListAsync();
    Task<Credit> GetByIdAsync(int id);
    Task<IEnumerable<Credit>> ListByClientIdAsync(int clientId);
    Task<IEnumerable<Credit>> ListByShopkeeperIdAsync(int shopkeeperId);
    Task<CreditResponse> SaveAsync(Credit credit);
    Task<CreditResponse> UpdateAsync(int id, Credit credit);
}