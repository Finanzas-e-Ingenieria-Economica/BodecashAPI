namespace BodecashAPI.Bodecash.Resources.Get;

public class NPPurchaseResource
{
    public int Id { get; set; }
    public DateTime PurchaseDate { get; set; }
    public decimal TotalValue { get; set; }
    
    public int NormalPurchaseId { get; set; }
}