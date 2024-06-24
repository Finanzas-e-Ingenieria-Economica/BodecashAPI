using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Shared.Domain.Services.Communication;

namespace BodecashAPI.Bodecash.Domain.Communication;

public class NPPurchaseProductResponse: BaseResponse<NPPurchaseProduct>
{
    public NPPurchaseProductResponse(string message) : base(message)
    {
    }

    public NPPurchaseProductResponse(NPPurchaseProduct resource) : base(resource)
    {
    }
}