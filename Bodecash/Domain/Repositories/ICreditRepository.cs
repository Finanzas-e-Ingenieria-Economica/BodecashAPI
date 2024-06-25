using BodecashAPI.Bodecash.Domain.Models;

namespace BodecashAPI.Bodecash.Domain.Repositories;

public interface ICreditRepository
{
    Task<IEnumerable<Credit>> ListAsync();
    Task<Credit> FindByIdAsync(int id);
    Task<IEnumerable<Credit>> ListByClientIdAsync(int clientId);
    Task<IEnumerable<Credit>> ListByShopkeeperIdAsync(int shopkeeperId);
    Task<Credit> FindUnpaidByClientId(int id);
    Task AddAsync(Credit credit);
    void Update(Credit credit);
}