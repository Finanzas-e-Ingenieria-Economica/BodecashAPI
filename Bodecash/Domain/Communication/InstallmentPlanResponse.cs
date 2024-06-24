using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Shared.Domain.Services.Communication;

namespace BodecashAPI.Bodecash.Domain.Communication;

public class InstallmentPlanResponse : BaseResponse<InstallmentPlan>
{
    public InstallmentPlanResponse(string message) : base(message)
    {
    }

    public InstallmentPlanResponse(InstallmentPlan resource) : base(resource)
    {
    }
}