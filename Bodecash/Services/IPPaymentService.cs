using BodecashAPI.Bodecash.Domain.Communication;
using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Bodecash.Domain.Repositories;
using BodecashAPI.Bodecash.Domain.Services;
using BodecashAPI.Shared.Persistence.Repositories;

namespace BodecashAPI.Bodecash.Services;

public class IPPaymentService : IIPPaymentService
{
    private readonly IIPPaymentRepository _iPPaymentRepository;
    private readonly IInstallmentPlanRepository _installmentPlanRepository;
    private readonly ICreditService _creditService;
    private readonly IUnitOfWork _unitOfWork;

    public IPPaymentService(IIPPaymentRepository iPPaymentRepository, IUnitOfWork unitOfWork, IInstallmentPlanRepository installmentPlanRepository, ICreditService creditService)
    {
        _iPPaymentRepository = iPPaymentRepository;
        _unitOfWork = unitOfWork;
        _installmentPlanRepository = installmentPlanRepository;
        _creditService = creditService;
    }

    public async Task<IEnumerable<IPPayment>> ListAsync()
    {
        return await _iPPaymentRepository.ListAsync();
    }

    public async Task<IPPayment> GetByIdAsync(int id)
    {
        return await _iPPaymentRepository.FindByIdAsync(id);
    }

    public async Task<IPPayment> GetByInstallmentPlanIdAndPositionAsync(int installmentPlanId, int position)
    {
        return await _iPPaymentRepository.GetByInstallmentPlanIdAndPositionAsync(installmentPlanId, position);
    }

    public async Task<IPPaymentResponse> SaveAsync(IPPayment resource)
    {
        try
        {
            await _iPPaymentRepository.AddAsync(resource);
            await _unitOfWork.CompleteAsync();
            return new IPPaymentResponse(resource);
        }
        catch (Exception e)
        {
            return new IPPaymentResponse("An error occurred when saving the payment: " + e.Message);
        }
    }

    public async Task<IPPaymentResponse> Pagar(int id)
    {
        try
        {
            if (id != 1)
            {
                var previousPaymentId = id - 1;
                var isPreviousPaymentPaid = await _iPPaymentRepository.IsPaymentPaid(previousPaymentId);
                
                if (!isPreviousPaymentPaid)
                    return new IPPaymentResponse("The previous payment must be paid first.");
            }
            
            var existingPayment = await _iPPaymentRepository.FindByIdAsync(id);
            
            if (existingPayment == null)
                    return new IPPaymentResponse("Payment not found.");
            
            existingPayment.IsPaid = true;

            _iPPaymentRepository.Update(existingPayment);
            await _unitOfWork.CompleteAsync();
            
            
            var existingInstallmentPlan = await _installmentPlanRepository.FindByIdAsync(existingPayment.InstallmentPlanId);
            existingInstallmentPlan.CurrentTerm += 1;
            
            _installmentPlanRepository.Update(existingInstallmentPlan);
            await _unitOfWork.CompleteAsync();
            
            return new IPPaymentResponse(existingPayment);
        }
        catch (Exception e)
        {
            return new IPPaymentResponse("An error occurred when saving the payment: " + e.Message);
        }
    }

    public async Task<IPPaymentResponse> AplicarTasaMoratoria(int id)
    {
        try
        {
            var existingPayment = await _iPPaymentRepository.FindByIdAsync(id);

            if (existingPayment == null)
                return new IPPaymentResponse("Payment not found.");

            var existingInstallmentPlan =
                await _installmentPlanRepository.FindByIdAsync(existingPayment.InstallmentPlanId);

            if (existingInstallmentPlan == null)
                return new IPPaymentResponse("Installment plan not found.");

            var existingCredit = await _creditService.GetByIdAsync(existingInstallmentPlan.CreditId);

            if (existingInstallmentPlan == null)
                return new IPPaymentResponse("Credit not found.");

            var valorMora = existingPayment.Fee * existingCredit.PenaltyInterestRate;
            var nuevaCuota = existingPayment.Fee + valorMora;
            existingPayment.Fee = nuevaCuota;
            
            _iPPaymentRepository.Update(existingPayment);
            await _unitOfWork.CompleteAsync();
            return new IPPaymentResponse(existingPayment);
        }
        catch (Exception e)
        {
            return new IPPaymentResponse("An error ocurred when updating the payment: " + e.Message);
        }
    }
}