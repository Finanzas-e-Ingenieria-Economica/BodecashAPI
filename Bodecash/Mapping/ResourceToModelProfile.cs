using AutoMapper;
using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Bodecash.Resources.Post;

namespace BodecashAPI.Bodecash.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveClientResource, Client>();
        CreateMap<SaveClientResource, PersonalData>();
        CreateMap<SaveCreditResource, Credit>();
        CreateMap<SaveInstallmentPlanResource, InstallmentPlan>();
        CreateMap<SaveInstallmentPlanResource, Credit>();
        CreateMap<SaveIPPaymentProductResource, IPPaymentProduct>();
        CreateMap<SaveIPPaymentResource, IPPayment>();
        CreateMap<SaveNormalPurchaseResource, NormalPurchase>();
        CreateMap<SaveNormalPurchaseResource, Credit>();
        CreateMap<SaveNPPurchaseProductResource, NPPurchaseProduct>();
        CreateMap<SaveNPPurchaseResource, NPPurchase>();
        CreateMap<SavePersonalDataResource, PersonalData>();
        CreateMap<SaveShopkeeperResource, Shopkeeper>();
        CreateMap<SaveShopkeeperResource, PersonalData>();
    }
}