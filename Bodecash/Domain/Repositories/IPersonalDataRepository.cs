using BodecashAPI.Bodecash.Domain.Models;

namespace BodecashAPI.Bodecash.Domain.Repositories;

public interface IPersonalDataRepository
{
    Task<IEnumerable<PersonalData>> ListAsync();
    Task<PersonalData> FindByIdAsync(int id);
    Task<PersonalData> FindByEmailAsync(string email);
    Task<PersonalData> FindByDNIAsync(string dni);
    Task<PersonalData> FindByCredentialsAsync(string email, string password);
    Task AddAsync(PersonalData personalData);
    void Remove(PersonalData personalData);
}