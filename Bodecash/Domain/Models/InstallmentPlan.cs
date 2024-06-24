namespace BodecashAPI.Bodecash.Domain.Models;

public class InstallmentPlan
{
    public int Id { get; set; }
    public decimal Fee { get; set; }
    public bool IsGracePeriod { get; set; }
    public string? GracePeriodType { get; set; }
    public int GracePeriodPeriods { get; set; }
    public int TotalTerm { get; set; }
}