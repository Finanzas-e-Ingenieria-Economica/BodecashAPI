using BodecashAPI.Bodecash.Domain.Communication;
using BodecashAPI.Bodecash.Domain.Models;

namespace BodecashAPI.Bodecash.Domain.Services;

public interface IInstallmentPlanService
{
    Task<IEnumerable<InstallmentPlan>> ListAsync();
    Task<InstallmentPlan> GetByIdAsync(int id);
    Task<IEnumerable<InstallmentPlan>> ListByCreditIdAsync(int clientId);
    Task<InstallmentPlanResponse> SaveAsync(InstallmentPlan installmentPlan, Credit credit);
}