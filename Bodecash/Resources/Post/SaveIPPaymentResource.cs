namespace BodecashAPI.Bodecash.Resources.Post;

public class SaveIPPaymentResource
{
    public decimal Capital { get; set; }
    public decimal Interest { get; set; }
    public decimal Fee { get; set; }
    public decimal Amortization { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsPaid { get; set; }
    public int? DaysPastDue { get; set; }
    public DateTime PaymentDate { get; set; }
    
    public int InstallmentPlanId { get; set; }
}