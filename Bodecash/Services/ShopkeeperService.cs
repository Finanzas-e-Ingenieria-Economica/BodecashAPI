using BodecashAPI.Bodecash.Domain.Communication;
using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Bodecash.Domain.Repositories;
using BodecashAPI.Bodecash.Domain.Services;
using BodecashAPI.Shared.Persistence.Repositories;

namespace BodecashAPI.Bodecash.Services;

public class ShopkeeperService : IShopkeeperService
{
    private readonly IShopkeeperRepository _shopkeeperRepository;
    private readonly IPersonalDataRepository _personalDataRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ShopkeeperService(IShopkeeperRepository shopkeeperRepository, IUnitOfWork unitOfWork, IPersonalDataRepository personalDataRepository)
    {
        _shopkeeperRepository = shopkeeperRepository;
        _unitOfWork = unitOfWork;
        _personalDataRepository = personalDataRepository;
    }

    public async Task<IEnumerable<Shopkeeper>> ListAsync()
    {
        return await _shopkeeperRepository.ListAsync();
    }

    public async Task<Shopkeeper> GetByIdAsync(int id)
    {
        return await _shopkeeperRepository.FindByIdAsync(id);
    }

    public async Task<Shopkeeper> GetByPersonalDataIdAsync(int id)
    {
        return await _shopkeeperRepository.GetByPersonalDataIdAsync(id);
    }

    public async Task<ShopkeeperResponse> SaveAsync(Shopkeeper shopkeeper, PersonalData personalData)
    {
        try
        {
            await _personalDataRepository.AddAsync(personalData);
            await _unitOfWork.CompleteAsync();
            
            shopkeeper.PersonalDataId = personalData.Id;
            await _shopkeeperRepository.AddAsync(shopkeeper);
            await _unitOfWork.CompleteAsync();
            return new ShopkeeperResponse(shopkeeper);
        }
        catch (Exception e)
        {
            return new ShopkeeperResponse("An error occurred when saving the shopkeeper: " + e.Message);
        }
    }
}