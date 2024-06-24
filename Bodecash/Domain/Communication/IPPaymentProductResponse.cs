using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Shared.Domain.Services.Communication;

namespace BodecashAPI.Bodecash.Domain.Communication;

public class IPPaymentProductResponse : BaseResponse<IPPaymentProduct>
{
    public IPPaymentProductResponse(string message) : base(message)
    {
    }

    public IPPaymentProductResponse(IPPaymentProduct resource) : base(resource)
    {
    }
}