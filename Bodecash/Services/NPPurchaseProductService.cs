using BodecashAPI.Bodecash.Domain.Communication;
using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Bodecash.Domain.Repositories;
using BodecashAPI.Bodecash.Domain.Services;
using BodecashAPI.Shared.Persistence.Repositories;

namespace BodecashAPI.Bodecash.Services;

public class NPPurchaseProductService : INPPurchaseProductService
{
    private readonly INPPurchaseProductRepository _nPPurchaseProductRepository;
    private readonly INPPurchaseRepository _npPurchaseRepository;
    private readonly INormalPurchaseRepository _normalPurchaseRepository;
    private readonly ICreditRepository _creditRepository;
    private readonly IUnitOfWork _unitOfWork;

    public NPPurchaseProductService(INPPurchaseProductRepository nPPurchaseProductRepository, IUnitOfWork unitOfWork, INPPurchaseRepository npPurchaseRepository, INormalPurchaseRepository normalPurchaseRepository, ICreditRepository creditRepository)
    {
        _nPPurchaseProductRepository = nPPurchaseProductRepository;
        _unitOfWork = unitOfWork;
        _npPurchaseRepository = npPurchaseRepository;
        _normalPurchaseRepository = normalPurchaseRepository;
        _creditRepository = creditRepository;
    }

    public async Task<IEnumerable<NPPurchaseProduct>> ListAsync()
    {
        return await _nPPurchaseProductRepository.ListAsync();
    }

    public async Task<NPPurchaseProduct> GetByIdAsync(int id)
    {
        return await _nPPurchaseProductRepository.FindByIdAsync(id);
    }

    public async Task<IEnumerable<NPPurchaseProduct>> GetByNPPurchaseIdAsync(int id)
    {
        return await _nPPurchaseProductRepository.GetByNPPurchaseIdAsync(id);
    }

    public async Task<NPPurchaseProductResponse> SaveAsync(NPPurchaseProduct nPPurchaseProduct)
    {
        try
        {
            
            var existingNPPurchase = await _npPurchaseRepository.FindByIdAsync(nPPurchaseProduct.NPPurchaseId);
            if (existingNPPurchase == null)
                return new NPPurchaseProductResponse("Not found.");

            var existingNormalPurchase =
                await _normalPurchaseRepository.FindByIdAsync(existingNPPurchase.NormalPurchaseId);
            if (existingNormalPurchase == null)
                return new NPPurchaseProductResponse("Not found.");
            
            var existingCredit = await _creditRepository.FindByIdAsync(existingNormalPurchase.CreditId);
            if (existingCredit == null)
                return new NPPurchaseProductResponse("Not found.");
            
            var productsValue = nPPurchaseProduct.Value * nPPurchaseProduct.Quantity;
            
            if(existingCredit.RemainingCredit - productsValue < 0)
                return new NPPurchaseProductResponse("Insufficient credit.");
            
            await _nPPurchaseProductRepository.AddAsync(nPPurchaseProduct);
            await _unitOfWork.CompleteAsync();
            
            var newTotalValue = existingNPPurchase.TotalValue + productsValue;
            
            existingNPPurchase.TotalValue += newTotalValue;
            
            _npPurchaseRepository.Update(existingNPPurchase);
            await _unitOfWork.CompleteAsync();
            return new NPPurchaseProductResponse(nPPurchaseProduct);
        }
        catch (Exception e)
        {
            return new NPPurchaseProductResponse("An error occurred when saving the nPPurchaseProduct: " + e.Message);
        }
    }
}