using BodecashAPI.Bodecash.Domain.Communication;
using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Bodecash.Domain.Repositories;
using BodecashAPI.Bodecash.Domain.Services;
using BodecashAPI.Shared.Persistence.Repositories;

namespace BodecashAPI.Bodecash.Services;

public class PersonalDataService : IPersonalDataService
{
    private readonly IPersonalDataRepository _personalDataRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PersonalDataService(IPersonalDataRepository personalDataRepository, IUnitOfWork unitOfWork)
    {
        _personalDataRepository = personalDataRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<PersonalData>> ListAsync()
    {
        return await _personalDataRepository.ListAsync();
    }

    public async Task<PersonalData> GetByIdAsync(int id)
    {
        return await _personalDataRepository.FindByIdAsync(id);
    }

    public async Task<PersonalDataResponse> SaveAsync(PersonalData personalData)
    {
        var existingPersonalData = await _personalDataRepository.FindByEmailAsync(personalData.Email);
        var existingPersonalDataByDNI = await _personalDataRepository.FindByDNIAsync(personalData.DNI);
        
        if (existingPersonalData != null)
            return new PersonalDataResponse("A personal data with that email already exists.");
        
        if (existingPersonalDataByDNI != null)
            return new PersonalDataResponse("A personal data with that DNI already exists.");
        
        try
        {
            await _personalDataRepository.AddAsync(personalData);
            await _unitOfWork.CompleteAsync();
            return new PersonalDataResponse(personalData);
        }
        catch (Exception e)
        {
            return new PersonalDataResponse($"An error occurred when saving the payment schedule: {e.Message}");
        }
    }

    public async Task<PersonalData> VerifyCredentialsAsync(string email, string password)
    {
        var existingPersonalData = await _personalDataRepository.FindByCredentialsAsync(email, password);
        
        if (existingPersonalData == null)
            return null;
        
        return existingPersonalData;
    }

    public async Task<PersonalDataResponse> DeleteAsync(int id)
    {
        var existingPersonalData = await _personalDataRepository.FindByIdAsync(id);
        
        if (existingPersonalData == null)
            return new PersonalDataResponse("Payment schedule not found. It may not exist.");
        
        try
        {
            _personalDataRepository.Remove(existingPersonalData);
            await _unitOfWork.CompleteAsync();
            return new PersonalDataResponse(existingPersonalData);
        }
        catch (Exception e)
        {
            return new PersonalDataResponse($"An error occurred when deleting the payment schedule: {e.Message}");
        }
    }
}