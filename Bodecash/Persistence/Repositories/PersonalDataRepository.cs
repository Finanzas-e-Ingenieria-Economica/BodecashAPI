using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Bodecash.Domain.Repositories;
using BodecashAPI.Shared.Persistence.Contexts;
using BodecashAPI.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BodecashAPI.Bodecash.Persistence.Repositories;

public class PersonalDataRepository : BaseRepository, IPersonalDataRepository
{
    public PersonalDataRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<PersonalData>> ListAsync()
    {
        return await _context.PersonalDatas.ToListAsync();
    }

    public async Task<PersonalData> FindByIdAsync(int id)
    {
        return await _context.PersonalDatas.FindAsync(id);
    }

    public async Task<PersonalData> FindByEmailAsync(string email)
    {
        return await _context.PersonalDatas.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<PersonalData> FindByDNIAsync(string dni)
    {
        return await _context.PersonalDatas.FirstOrDefaultAsync(x => x.DNI == dni);
    }

    public async Task<PersonalData> FindByCredentialsAsync(string email, string password)
    {
        return await _context.PersonalDatas
            .FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
    }

    public async Task AddAsync(PersonalData personalData)
    {
        await _context.PersonalDatas.AddAsync(personalData);
    }

    public void Remove(PersonalData personalData)
    {
        _context.PersonalDatas.Remove(personalData);
    }
}