using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Shared.Domain.Services.Communication;

namespace BodecashAPI.Bodecash.Domain.Communication;

public class ShopkeeperResponse : BaseResponse<Shopkeeper>
{
    public ShopkeeperResponse(string message) : base(message)
    {
    }

    public ShopkeeperResponse(Shopkeeper resource) : base(resource)
    {
    }
}