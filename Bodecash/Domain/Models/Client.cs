namespace BodecashAPI.Bodecash.Domain.Models;

public class Client
{
    public int Id { get; set; }
    
    public int PersonalDataId { get; set; }
    public PersonalData PersonalData { get; set; }
    public IList<Credit> Credits { get; set; }
}