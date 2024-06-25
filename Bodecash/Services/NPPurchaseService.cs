using BodecashAPI.Bodecash.Domain.Communication;
using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Bodecash.Domain.Repositories;
using BodecashAPI.Bodecash.Domain.Services;
using BodecashAPI.Shared.Persistence.Repositories;

namespace BodecashAPI.Bodecash.Services;

public class NPPurchaseService : INPPurchaseService
{
    private readonly INPPurchaseRepository _nPPurchaseRepository;
    private readonly IUnitOfWork _unitOfWork;

    public NPPurchaseService(INPPurchaseRepository nPPurchaseRepository, IUnitOfWork unitOfWork)
    {
        _nPPurchaseRepository = nPPurchaseRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<NPPurchase>> ListAsync()
    {
        return await _nPPurchaseRepository.ListAsync();
    }

    public async Task<NPPurchase> GetByIdAsync(int id)
    {
        return await _nPPurchaseRepository.FindByIdAsync(id);
    }

    public async Task<IEnumerable<NPPurchase>> ListByNormalPurchaseIdAsync(int normalPurchaseId)
    {
        return await _nPPurchaseRepository.ListByNormalPurchaseIdAsync(normalPurchaseId);
    }

    public async Task<NPPurchaseResponse> SaveAsync(NPPurchase nPPurchase)
    {
        try
        {
            await _nPPurchaseRepository.AddAsync(nPPurchase);
            await _unitOfWork.CompleteAsync();
            return new NPPurchaseResponse(nPPurchase);
        }
        catch (Exception e)
        {
            return new NPPurchaseResponse($"An error occurred when saving the nPPurchase: {e.Message}");
        }
    }
}