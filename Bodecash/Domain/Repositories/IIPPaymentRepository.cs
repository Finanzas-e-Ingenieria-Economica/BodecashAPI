using BodecashAPI.Bodecash.Domain.Models;

namespace BodecashAPI.Bodecash.Domain.Repositories;

public interface IIPPaymentRepository
{
    Task<IEnumerable<IPPayment>> ListAsync();
    Task<IPPayment> FindByIdAsync(int id);
    Task<IPPayment> GetByInstallmentPlanIdAndPositionAsync(int id, int position);
    Task AddAsync(IPPayment IPPayment);
    Task<bool> IsPaymentPaid(int id);
    void Update(IPPayment IPPayment);
}