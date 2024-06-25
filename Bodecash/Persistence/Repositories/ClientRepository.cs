using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Bodecash.Domain.Repositories;
using BodecashAPI.Shared.Persistence.Contexts;
using BodecashAPI.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BodecashAPI.Bodecash.Persistence.Repositories;

public class ClientRepository : BaseRepository, IClientRepository
{
    public ClientRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Client>> ListAsync()
    {
        return await _context.Clients.ToListAsync();
    }

    public async Task<Client> FindByIdAsync(int id)
    {
        return await _context.Clients.FindAsync(id);
    }

    public async Task AddAsync(Client client)
    {
        await _context.Clients.AddAsync(client);
    }
}