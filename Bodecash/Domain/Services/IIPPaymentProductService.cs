using BodecashAPI.Bodecash.Domain.Communication;
using BodecashAPI.Bodecash.Domain.Models;

namespace BodecashAPI.Bodecash.Domain.Services;

public interface IIPPaymentProductService
{
    Task<IEnumerable<IPPaymentProduct>> ListAsync();
    Task<IPPaymentProduct> GetByIdAsync(int id);
    Task<IEnumerable<IPPaymentProduct>> GetByIPPaymentIdAsync(int id);
    Task<IPPaymentProductResponse> SaveAsync(IPPaymentProduct iPPaymentProduct);
}