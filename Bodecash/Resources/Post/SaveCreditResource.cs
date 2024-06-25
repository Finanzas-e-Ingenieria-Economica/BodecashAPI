namespace BodecashAPI.Bodecash.Resources.Post;

public class SaveCreditResource
{
    public int InterestRate { get; set; }
    public string InterestType { get; set; }
    public string? Capitalization { get; set; }
    public decimal PenaltyInterestRate { get; set; }
    public DateTime DisbursementDate { get; set; }
    public string Type { get; set; }
    public decimal CreditAmount { get; set; }
    public decimal UsedCredit { get; set; }
    public decimal RemainingCredit { get; set; }
    public bool IsPayed { get; set; }
    
    public int ShopkeeperId { get; set; }
    public int ClientId { get; set; }
}