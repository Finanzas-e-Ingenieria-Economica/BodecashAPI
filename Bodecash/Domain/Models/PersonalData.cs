namespace BodecashAPI.Bodecash.Domain.Models;

public class PersonalData
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string DNI { get; set; }
    public string UserType { get; set; }
    
    public Shopkeeper Shopkeeper { get; set; }
    public Client Client { get; set; }
}