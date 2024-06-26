using BodecashAPI.Bodecash.Domain.Models;

namespace BodecashAPI.Bodecash.Domain.Repositories;

public interface IShopkeeperRepository
{
    Task<IEnumerable<Shopkeeper>> ListAsync();
    Task<Shopkeeper> FindByIdAsync(int id);
    Task<Shopkeeper> GetByPersonalDataIdAsync(int id);
    Task AddAsync(Shopkeeper shopkeeper);
}