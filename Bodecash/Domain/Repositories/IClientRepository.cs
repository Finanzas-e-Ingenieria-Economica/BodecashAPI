using BodecashAPI.Bodecash.Domain.Models;

namespace BodecashAPI.Bodecash.Domain.Repositories;

public interface IClientRepository
{
    Task<IEnumerable<Client>> ListAsync();
    Task<Client> FindByIdAsync(int id);
    Task<Client> GetByPersonalDataIdAsync(int id);
    Task AddAsync(Client client);
}