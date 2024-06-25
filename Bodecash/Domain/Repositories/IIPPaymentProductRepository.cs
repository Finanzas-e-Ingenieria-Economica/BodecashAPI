using BodecashAPI.Bodecash.Domain.Models;

namespace BodecashAPI.Bodecash.Domain.Repositories;

public interface IIPPaymentProductRepository
{
    Task<IEnumerable<IPPaymentProduct>> ListAsync();
    Task<IPPaymentProduct> FindByIdAsync(int id);
    Task<IEnumerable<IPPaymentProduct>> GetByIPPaymentIdAsync(int id);
    Task AddAsync(IPPaymentProduct IPPaymentProduct);
}