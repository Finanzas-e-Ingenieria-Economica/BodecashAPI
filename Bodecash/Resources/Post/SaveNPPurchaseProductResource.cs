namespace BodecashAPI.Bodecash.Resources.Post;

public class SaveNPPurchaseProductResource
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Value { get; set; }
    
    public int NPPurchaseId { get; set; }
}