namespace BodecashAPI.Bodecash.Domain.Models;

public class Credit
{
    public int Id { get; set; }
    public int InterestRate { get; set; }
    public string InterestType { get; set; }
    public string? Capitalization { get; set; }
    public decimal PenaltyInterestRate { get; set; }
    public DateTime DisbursementDate { get; set; }
    public string Type { get; set; }
    public decimal CreditAmount { get; set; }
    public decimal UsedCredit { get; set; }
    public decimal RemainingCredit { get; set; }
    public bool IsCreditPayed { get; set; }
    
    public int ShopkeeperId { get; set; }
    public Shopkeeper Shopkeeper { get; set; }
    public int ClientId { get; set; }
    public Client Client { get; set; }
    public IList<InstallmentPlan> InstallmentPlans { get; set; }
    public IList<NormalPurchase> NormalPurchases { get; set; }
}