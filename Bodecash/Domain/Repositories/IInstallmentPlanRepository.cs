using BodecashAPI.Bodecash.Domain.Models;

namespace BodecashAPI.Bodecash.Domain.Repositories;

public interface IInstallmentPlanRepository
{
    Task<IEnumerable<InstallmentPlan>> ListAsync();
    Task<InstallmentPlan> FindByIdAsync(int id);
    Task<IEnumerable<InstallmentPlan>> ListByCreditIdAsync(int clientId);
    Task AddAsync(InstallmentPlan installmentPlan);
    void Update(InstallmentPlan installmentPlan);
}