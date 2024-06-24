namespace BodecashAPI.Bodecash.Domain.Models;

public class NPPurchaseProduct
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Value { get; set; }
    
    public int NPPurchaseId { get; set; }
    public NPPurchase NPPurchase { get; set; }
}