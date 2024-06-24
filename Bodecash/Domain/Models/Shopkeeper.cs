namespace BodecashAPI.Bodecash.Domain.Models;

public class Shopkeeper
{
    public int Id { get; set; }
    public string Store { get; set; }
    
    public int PersonalDataId { get; set; }
    public PersonalData PersonalData { get; set; }
    public IList<Credit> Credits { get; set; }
}