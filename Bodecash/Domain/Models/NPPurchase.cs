namespace BodecashAPI.Bodecash.Domain.Models;

public class NPPurchase
{
    public int Id { get; set; }
    public DateTime PurchaseDate { get; set; }
    public decimal TotalValue { get; set; }
    
    public int NormalPurchaseId { get; set; }
    public NormalPurchase NormalPurchase { get; set; }
    public IList<NPPurchaseProduct> NPPurchaseProducts { get; set; }
}