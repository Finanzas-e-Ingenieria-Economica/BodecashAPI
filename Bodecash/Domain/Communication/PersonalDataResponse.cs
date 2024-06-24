using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Shared.Domain.Services.Communication;

namespace BodecashAPI.Bodecash.Domain.Communication;

public class PersonalDataResponse : BaseResponse<PersonalData>
{
    public PersonalDataResponse(string message) : base(message)
    {
    }

    public PersonalDataResponse(PersonalData resource) : base(resource)
    {
    }
}