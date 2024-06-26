namespace BodecashAPI.Bodecash.Domain.Models;

public class NormalPurchase
{
    public int Id { get; set; }
    public decimal AmountDue { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? PaymentDate { get; set; }
    public bool IsPaid { get; set; }
    public int? DaysPastDue { get; set; }
    
    public int CreditId { get; set; }
    public Credit Credit { get; set; }
    public IList<NPPurchase> NPPurchases { get; set; }
}