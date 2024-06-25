using BodecashAPI.Bodecash.Domain.Models;
using BodecashAPI.Bodecash.Domain.Repositories;
using BodecashAPI.Shared.Persistence.Contexts;
using BodecashAPI.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BodecashAPI.Bodecash.Persistence.Repositories;

public class IPPaymentRepository : BaseRepository, IIPPaymentRepository
{
    public IPPaymentRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<IPPayment>> ListAsync()
    {
        return await _context.IPPayments.ToListAsync();
    }

    public async Task<IPPayment> FindByIdAsync(int id)
    {
        return await _context.IPPayments.FindAsync(id);
    }

    public async Task<IPPayment> GetByInstallmentPlanIdAndPositionAsync(int id, int position)
    {
        // Filtrar por InstallmentPlanId y ordenar por algún campo (ej. Fecha de Pago o ID)
        var query = _context.IPPayments
            .Where(payment => payment.InstallmentPlanId == id)
            .OrderBy(payment => payment.Id); // Ajusta el campo de ordenamiento según tus necesidades

        // Obtener el registro en la posición especificada (considerando 0-based index)
        var payment = await query.Skip(position - 1).FirstOrDefaultAsync();
    
        return payment;
    }

    public async Task AddAsync(IPPayment IPPayment)
    {
        await _context.IPPayments.AddAsync(IPPayment);
    }

    public async Task<bool> IsPaymentPaid(int id)
    {
        var payment = await _context.IPPayments.FindAsync(id);

        return payment.IsPaid;
    }

    public void Update(IPPayment IPPayment)
    {
        _context.Update(IPPayment);
    }
}