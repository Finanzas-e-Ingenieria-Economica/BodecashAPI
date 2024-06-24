namespace BodecashAPI.Bodecash.Resources.Get;

public class CreditResource
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
    
    public int ShopkeeperId { get; set; }
    public int ClientId { get; set; }
}