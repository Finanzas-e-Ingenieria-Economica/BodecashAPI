using BodecashAPI.Bodecash.Domain.Communication;
using BodecashAPI.Bodecash.Domain.Models;

namespace BodecashAPI.Bodecash.Domain.Services;

public interface IShopkeeperService
{
    Task<IEnumerable<Shopkeeper>> ListAsync();
    Task<Shopkeeper> GetByIdAsync(int id);
    Task<ShopkeeperResponse> SaveAsync(Shopkeeper shopkeeper, PersonalData personalData);
}