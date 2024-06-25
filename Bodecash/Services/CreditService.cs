using BodecashAPI.Bodecash.Domain.Communication;
using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Bodecash.Domain.Repositories;
using BodecashAPI.Bodecash.Domain.Services;
using BodecashAPI.Shared.Persistence.Repositories;

namespace BodecashAPI.Bodecash.Services;

public class CreditService : ICreditService
{
    private readonly ICreditRepository _creditRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreditService(ICreditRepository creditRepository, IUnitOfWork unitOfWork)
    {
        _creditRepository = creditRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Credit>> ListAsync()
    {
        return await _creditRepository.ListAsync();
    }

    public async Task<Credit> GetByIdAsync(int id)
    {
        return await _creditRepository.FindByIdAsync(id);
    }

    public async Task<IEnumerable<Credit>> ListByClientIdAsync(int clientId)
    {
        return await _creditRepository.ListByClientIdAsync(clientId);
    }

    public async Task<IEnumerable<Credit>> ListByShopkeeperIdAsync(int shopkeeperId)
    {
        return await _creditRepository.ListByShopkeeperIdAsync(shopkeeperId);
    }

    public async Task<CreditResponse> SaveAsync(Credit credit)
    {
        try
        {
            await _creditRepository.AddAsync(credit);
            await _unitOfWork.CompleteAsync();
            return new CreditResponse(credit);
        }
        catch (Exception e)
        {
            return new CreditResponse("An error occurred when saving the credit: " + e.Message);
        }
    }

    public async Task<CreditResponse> UpdateAsync(int id, Credit credit)
    {
        try
        {
            var existingCredit = await _creditRepository.FindByIdAsync(id);
            if (existingCredit == null)
                return new CreditResponse("Credit not found.");
        
            existingCredit.IsCreditPayed = credit.IsCreditPayed;
            
            _creditRepository.Update(existingCredit);
            await _unitOfWork.CompleteAsync();
            return new CreditResponse(existingCredit);
        }
        catch (Exception e)
        {
            return new CreditResponse($"An error occurred while updating credit: {e.Message}");
        }
    }
}