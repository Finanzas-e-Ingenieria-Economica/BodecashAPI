using BodecashAPI.Bodecash.Domain.Communication;
using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Bodecash.Domain.Repositories;
using BodecashAPI.Bodecash.Domain.Services;
using BodecashAPI.Shared.Persistence.Repositories;

namespace BodecashAPI.Bodecash.Services;

public class InstallmentPlanService : IInstallmentPlanService
{
    private readonly IInstallmentPlanRepository _installmentPlanRepository;
    private readonly ICreditRepository _creditRepository;
    private readonly IUnitOfWork _unitOfWork;

    public InstallmentPlanService(IInstallmentPlanRepository installmentPlanRepository, IUnitOfWork unitOfWork, ICreditRepository creditRepository)
    {
        _installmentPlanRepository = installmentPlanRepository;
        _unitOfWork = unitOfWork;
        _creditRepository = creditRepository;
    }

    public async Task<IEnumerable<InstallmentPlan>> ListAsync()
    {
       return await _installmentPlanRepository.ListAsync();
    }

    public async Task<InstallmentPlan> GetByIdAsync(int id)
    {
        return await _installmentPlanRepository.FindByIdAsync(id);
    }

    public async Task<IEnumerable<InstallmentPlan>> ListByCreditIdAsync(int clientId)
    {
        return await _installmentPlanRepository.ListByCreditIdAsync(clientId);
    }

    public async Task<InstallmentPlanResponse> SaveAsync(InstallmentPlan installmentPlan, Credit credit)
    {
        try
        {
            var existingUnpaidCredit = await _creditRepository.FindUnpaidByClientId(credit.ClientId);
            
            if (existingUnpaidCredit != null)
                return new InstallmentPlanResponse("Client already has an unpaid credit.");

            credit.IsCreditPayed = false;
            await _creditRepository.AddAsync(credit);
            await _unitOfWork.CompleteAsync();

            installmentPlan.CurrentTerm = 1;
            installmentPlan.CreditId = credit.Id;
            await _installmentPlanRepository.AddAsync(installmentPlan);
            await _unitOfWork.CompleteAsync();
            return new InstallmentPlanResponse(installmentPlan);
        }
        catch (Exception e)
        {
            return new InstallmentPlanResponse("An error occurred when saving the installment plan: " + e.Message);
        }
    }
}