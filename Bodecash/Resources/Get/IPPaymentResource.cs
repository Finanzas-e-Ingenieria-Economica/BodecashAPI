namespace BodecashAPI.Bodecash.Resources.Get;

public class IPPaymentResource
{
    public int Id { get; set; }
    public decimal Capital { get; set; }
    public decimal Interest { get; set; }
    public decimal Fee { get; set; }
    public decimal Amortization { get; set; }
    public decimal SaldoInicial { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsPaid { get; set; }
    public int? DaysPastDue { get; set; }
    public DateTime? PaymentDate { get; set; }
    
    public int InstallmentPlanId { get; set; }
}