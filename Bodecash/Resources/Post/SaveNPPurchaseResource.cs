namespace BodecashAPI.Bodecash.Resources.Post;

public class SaveNPPurchaseResource
{
    public DateTime PurchaseDate { get; set; }
    public decimal TotalValue { get; set; }
    
    public int NormalPurchaseId { get; set; }
}