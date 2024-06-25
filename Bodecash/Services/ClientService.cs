using BodecashAPI.Bodecash.Domain.Communication;
using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Bodecash.Domain.Repositories;
using BodecashAPI.Bodecash.Domain.Services;
using BodecashAPI.Bodecash.Persistence.Repositories;
using BodecashAPI.Shared.Persistence.Repositories;

namespace BodecashAPI.Bodecash.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IPersonalDataRepository _personalDataRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ClientService(IClientRepository clientRepository, IUnitOfWork unitOfWork, IPersonalDataRepository personalDataRepository)
    {
        _clientRepository = clientRepository;
        _unitOfWork = unitOfWork;
        _personalDataRepository = personalDataRepository;
    }

    public async Task<IEnumerable<Client>> ListAsync()
    {
        return await _clientRepository.ListAsync();
    }

    public async Task<Client> GetByIdAsync(int id)
    {
        return await _clientRepository.FindByIdAsync(id);
    }

    public async Task<ClientResponse> SaveAsync(Client client, PersonalData personalData)
    {
        try
        {
            await _personalDataRepository.AddAsync(personalData);
            await _unitOfWork.CompleteAsync();
            client.PersonalDataId = personalData.Id;
            
            await _clientRepository.AddAsync(client);
            await _unitOfWork.CompleteAsync();
            return new ClientResponse(client);
        }
        catch (Exception e)
        {
            return new ClientResponse("An error occurred when saving the client: " + e.Message);
        }
    }
}