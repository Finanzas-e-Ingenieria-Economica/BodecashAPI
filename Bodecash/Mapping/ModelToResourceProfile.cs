using AutoMapper;
using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Bodecash.Resources.Get;

namespace BodecashAPI.Bodecash.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Client, ClientResource>();
        CreateMap<Credit, CreditResource>();
        CreateMap<InstallmentPlan, InstallmentPlanResource>();
        CreateMap<IPPaymentProduct, IPPaymentProductResource>();
        CreateMap<IPPayment, IPPaymentResource>();
        CreateMap<NormalPurchase, NormalPurchaseResource>();
        CreateMap<NPPurchaseProduct, NPPurchaseProductResource>();
        CreateMap<NPPurchase, NPPurchaseResource>();
        CreateMap<PersonalData, PersonalDataResource>();
        CreateMap<Shopkeeper, ShopkeeperResource>();
    }
}