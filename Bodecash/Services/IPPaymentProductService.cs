using BodecashAPI.Bodecash.Domain.Communication;
using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Bodecash.Domain.Repositories;
using BodecashAPI.Bodecash.Domain.Services;
using BodecashAPI.Shared.Persistence.Repositories;

namespace BodecashAPI.Bodecash.Services;

public class IPPaymentProductService : IIPPaymentProductService
{
    private readonly IIPPaymentProductRepository _iPPaymentProductRepository;
    private readonly IIPPaymentRepository _ipPaymentRepository;
    private readonly IInstallmentPlanRepository _installmentPlanRepository;
    private readonly ICreditRepository _creditRepository;
    private readonly IUnitOfWork _unitOfWork;

    public IPPaymentProductService(IIPPaymentProductRepository iPPaymentProductRepository, IUnitOfWork unitOfWork, IIPPaymentRepository ipPaymentRepository, IInstallmentPlanRepository installmentPlanRepository, ICreditRepository creditRepository)
    {
        _iPPaymentProductRepository = iPPaymentProductRepository;
        _unitOfWork = unitOfWork;
        _ipPaymentRepository = ipPaymentRepository;
        _installmentPlanRepository = installmentPlanRepository;
        _creditRepository = creditRepository;
    }

    public async Task<IEnumerable<IPPaymentProduct>> ListAsync()
    {
        return await _iPPaymentProductRepository.ListAsync();
    }

    public async Task<IPPaymentProduct> GetByIdAsync(int id)
    {
        return await _iPPaymentProductRepository.FindByIdAsync(id);
    }

    public async Task<IEnumerable<IPPaymentProduct>> GetByIPPaymentIdAsync(int id)
    {
        return await _iPPaymentProductRepository.GetByIPPaymentIdAsync(id);
    }

    public async Task<IPPaymentProductResponse> SaveAsync(IPPaymentProduct iPPaymentProduct)
    {
        try
        {
            var existingIPPayment = await _ipPaymentRepository.FindByIdAsync(iPPaymentProduct.IPPaymentId);
            if (existingIPPayment == null)
                return new IPPaymentProductResponse("IP Payment not found.");
            
            var existingInstallmentPlan = await _installmentPlanRepository.FindByIdAsync(existingIPPayment.InstallmentPlanId);
            if (existingInstallmentPlan == null)
                return new IPPaymentProductResponse("Installment plan not found.");
            
            var existingCredit = await _creditRepository.FindByIdAsync(existingInstallmentPlan.CreditId);
            if (existingCredit == null)
                return new IPPaymentProductResponse("Credit not found.");
            
            if (existingCredit.RemainingCredit - iPPaymentProduct.TotalValue < 0)
                return new IPPaymentProductResponse("Insufficient credit.");
            
            await _iPPaymentProductRepository.AddAsync(iPPaymentProduct);
            await _unitOfWork.CompleteAsync();
            return new IPPaymentProductResponse(iPPaymentProduct);
        }
        catch (Exception e)
        {
            return new IPPaymentProductResponse("An error occurred when saving the payment: " + e.Message);
        }
    }
}