using BodecashAPI.Bodecash.Domain.Communication;
using BodecashAPI.Bodecash.Domain.Models;

namespace BodecashAPI.Bodecash.Domain.Services;

public interface IPersonalDataService
{
    Task<IEnumerable<PersonalData>> ListAsync();
    Task<PersonalData> GetByIdAsync(int id);
    Task<PersonalDataResponse> SaveAsync(PersonalData personalData);
    Task<PersonalData> VerifyCredentialsAsync(string email, string password);
    Task<PersonalDataResponse> DeleteAsync(int id);
}