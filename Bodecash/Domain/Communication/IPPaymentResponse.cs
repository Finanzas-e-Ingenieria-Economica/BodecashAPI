using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Shared.Domain.Services.Communication;

namespace BodecashAPI.Bodecash.Domain.Communication;

public class IPPaymentResponse : BaseResponse<IPPayment>
{
    public IPPaymentResponse(string message) : base(message)
    {
    }

    public IPPaymentResponse(IPPayment resource) : base(resource)
    {
    }
}