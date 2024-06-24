using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Shared.Domain.Services.Communication;

namespace BodecashAPI.Bodecash.Domain.Communication;

public class NPPurchaseResponse : BaseResponse<NPPurchase>
{
    public NPPurchaseResponse(string message) : base(message)
    {
    }

    public NPPurchaseResponse(NPPurchase resource) : base(resource)
    {
    }
}