namespace BodecashAPI.Bodecash.Domain.Models;

public class InstallmentPlan
{
    public int Id { get; set; }
    public decimal Fee { get; set; }
    public bool IsGracePeriod { get; set; }
    public string? GracePeriodType { get; set; }
    public int? GracePeriodPeriods { get; set; }
    public int TotalTerm { get; set; }
    public string PaymentTimeType { get; set; }
    
    public int CreditId { get; set; }
    public Credit Credit { get; set; }
    public IList<IPPayment> IPPayments { get; set; }
}