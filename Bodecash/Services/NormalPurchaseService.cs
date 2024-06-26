using BodecashAPI.Bodecash.Domain.Communication;
using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Bodecash.Domain.Repositories;
using BodecashAPI.Bodecash.Domain.Services;
using BodecashAPI.Shared.Persistence.Repositories;

namespace BodecashAPI.Bodecash.Services;

public class NormalPurchaseService : INormalPurchaseService
{
    private readonly INormalPurchaseRepository _normalPurchaseRepository;
    private readonly ICreditRepository _creditRepository;
    private readonly IUnitOfWork _unitOfWork;

    public NormalPurchaseService(INormalPurchaseRepository normalPurchaseRepository, IUnitOfWork unitOfWork, ICreditRepository creditRepository)
    {
        _normalPurchaseRepository = normalPurchaseRepository;
        _unitOfWork = unitOfWork;
        _creditRepository = creditRepository;
    }

    public async Task<IEnumerable<NormalPurchase>> ListAsync()
    {
        return await _normalPurchaseRepository.ListAsync();
    }

    public async Task<NormalPurchase> GetByIdAsync(int id)
    {
        return await _normalPurchaseRepository.FindByIdAsync(id);
    }

    public async Task<IEnumerable<NormalPurchase>> ListByCreditIdAsync(int creditId)
    {
        return await _normalPurchaseRepository.ListByCreditIdAsync(creditId);
    }

    public async Task<NormalPurchaseResponse> SaveAsync(NormalPurchase normalPurchase, Credit credit)
    {
        try
        {
            /*var existingUnpaidCredit = await _creditRepository.FindUnpaidByClientId(credit.ClientId);
            
            if (existingUnpaidCredit != null)
                return new NormalPurchaseResponse("Client already has an unpaid credit.");*/

            credit.IsCreditPayed = false;
            await _creditRepository.AddAsync(credit);
            await _unitOfWork.CompleteAsync();

            normalPurchase.CreditId = credit.Id;
            normalPurchase.IsPaid = false;
            await _normalPurchaseRepository.AddAsync(normalPurchase);
            await _unitOfWork.CompleteAsync();
            return new NormalPurchaseResponse(normalPurchase);
        }
        catch (Exception e)
        {
            return new NormalPurchaseResponse("An error occurred when saving the normal purchase: " + e.Message);
        }
    }
}