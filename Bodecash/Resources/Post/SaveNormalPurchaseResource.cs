namespace BodecashAPI.Bodecash.Resources.Post;

public class SaveNormalPurchaseResource
{
    public decimal AmountDue { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime PaymentDate { get; set; }
    public bool IsPaid { get; set; }
    public int? DaysPastDue { get; set; }
    
    public int CreditId { get; set; }
}