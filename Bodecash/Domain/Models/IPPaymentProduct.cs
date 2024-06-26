namespace BodecashAPI.Bodecash.Domain.Models;

public class IPPaymentProduct
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal TotalValue { get; set; }
    public DateTime PurchaseDate { get; set; }
    
    public int IPPaymentId { get; set; }
    public IPPayment IPPayment { get; set; }
}