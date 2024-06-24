namespace BodecashAPI.Bodecash.Resources.Get;

public class NPPurchaseProductResource
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Value { get; set; }
    
    public int NPPurchaseId { get; set; }
}