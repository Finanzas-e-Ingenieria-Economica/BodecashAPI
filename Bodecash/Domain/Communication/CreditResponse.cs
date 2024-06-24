using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Shared.Domain.Services.Communication;

namespace BodecashAPI.Bodecash.Domain.Communication;

public class CreditResponse : BaseResponse<Credit>
{
    public CreditResponse(string message) : base(message)
    {
    }

    public CreditResponse(Credit resource) : base(resource)
    {
    }
}