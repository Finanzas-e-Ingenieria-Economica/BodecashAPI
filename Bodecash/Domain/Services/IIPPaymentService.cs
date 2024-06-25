using BodecashAPI.Bodecash.Domain.Communication;
using BodecashAPI.Bodecash.Domain.Models;

namespace BodecashAPI.Bodecash.Domain.Services;

public interface IIPPaymentService
{
    Task<IEnumerable<IPPayment>> ListAsync();
    Task<IPPayment> GetByIdAsync(int id);
    Task<IPPayment> GetByInstallmentPlanIdAndPositionAsync(int installmentPlanId, int position);
    Task<IPPaymentResponse> SaveAsync(IPPayment resource);
    Task<IPPaymentResponse> Pagar(int id);
    Task<IPPaymentResponse> AplicarTasaMoratoria(int id);
}