using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Bodecash.Domain.Repositories;
using BodecashAPI.Shared.Persistence.Contexts;
using BodecashAPI.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BodecashAPI.Bodecash.Persistence.Repositories;

public class InstallmentPlanRepository : BaseRepository, IInstallmentPlanRepository
{
    public InstallmentPlanRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<InstallmentPlan>> ListAsync()
    {
        return await _context.InstallmentPlans.ToListAsync();
    }

    public async Task<InstallmentPlan> FindByIdAsync(int id)
    {
        return await _context.InstallmentPlans.FindAsync(id);
    }

    public async Task<IEnumerable<InstallmentPlan>> ListByCreditIdAsync(int clientId)
    {
        return await _context.InstallmentPlans.Where(x => x.CreditId == clientId).ToListAsync();
    }

    public async Task AddAsync(InstallmentPlan installmentPlan)
    {
        await _context.InstallmentPlans.AddAsync(installmentPlan);
    }

    public void Update(InstallmentPlan installmentPlan)
    {
        _context.Update(installmentPlan);
    }
}