namespace BodecashAPI.Bodecash.Resources.Get;

public class IPPaymentProductResource
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal TotalValue { get; set; }
    public DateTime PurchaseDate { get; set; }
    
    public int IPPaymentId { get; set; }
}