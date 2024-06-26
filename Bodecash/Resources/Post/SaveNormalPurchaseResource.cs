namespace BodecashAPI.Bodecash.Resources.Post;

public class SaveNormalPurchaseResource
{
    public decimal AmountDue { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? PaymentDate { get; set; }
    public bool IsPaid { get; set; }
    public int? DaysPastDue { get; set; }
    
    public decimal InterestRate { get; set; }
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
    public int ClientId { get; set; }
}