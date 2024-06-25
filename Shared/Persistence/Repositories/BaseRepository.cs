using BodecashAPI.Shared.Persistence.Contexts;

namespace BodecashAPI.Shared.Persistence.Repositories;

public class BaseRepository
{
    protected readonly AppDbContext _context;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
    }
}