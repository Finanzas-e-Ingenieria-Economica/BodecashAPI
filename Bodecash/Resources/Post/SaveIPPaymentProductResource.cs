namespace BodecashAPI.Bodecash.Resources.Post;

public class SaveIPPaymentProductResource
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal TotalValue { get; set; }
    public DateTime PurchaseDate { get; set; }
    
    public int IPPaymentId { get; set; }
}