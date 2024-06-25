using BodecashAPI.Bodecash.Domain.Communication;
using BodecashAPI.Bodecash.Domain.Models;

namespace BodecashAPI.Bodecash.Domain.Services;

public interface IClientService
{
    Task<IEnumerable<Client>> ListAsync();
    Task<Client> GetByIdAsync(int id);
    Task<ClientResponse> SaveAsync(Client client, PersonalData personalData);
}