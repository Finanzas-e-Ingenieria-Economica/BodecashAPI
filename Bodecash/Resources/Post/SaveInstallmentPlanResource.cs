namespace BodecashAPI.Bodecash.Resources.Post;

public class SaveInstallmentPlanResource
{
    public decimal Fee { get; set; }
    public bool IsGracePeriod { get; set; }
    public string? GracePeriodType { get; set; }
    public int? GracePeriodPeriods { get; set; }
    public int TotalTerm { get; set; }
    public string PaymentTimeType { get; set; }
    public int CurrentTerm { get; set; }
    
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
    
    public int CreditId { get; set; }
}