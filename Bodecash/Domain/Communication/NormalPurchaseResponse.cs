using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Shared.Domain.Services.Communication;

namespace BodecashAPI.Bodecash.Domain.Communication;

public class NormalPurchaseResponse : BaseResponse<NormalPurchase>
{
    public NormalPurchaseResponse(string message) : base(message)
    {
    }

    public NormalPurchaseResponse(NormalPurchase resource) : base(resource)
    {
    }
}