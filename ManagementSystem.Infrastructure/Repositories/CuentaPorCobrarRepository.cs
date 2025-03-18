using ManagementSystem.Domain.Entities;
using ManagementSystem.Infrastructure.Data;
using ManagementSystem.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Infrastructure.Repositories
{
    public class CuentaPorCobrarRepository : ICuentaPorCobrarRepository
    {
        private readonly ApplicationDbContext _context;

        public CuentaPorCobrarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CuentaPorCobrar>> GetAllAsync()
        {
            return await _context.CuentasPorCobrar.ToListAsync();
        }

        public async Task<CuentaPorCobrar> GetByIdAsync(int id)
        {
            return await _context.CuentasPorCobrar.FindAsync(id);
        }

        public async Task AddAsync(CuentaPorCobrar cuenta)
        {
            _context.CuentasPorCobrar.Add(cuenta);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CuentaPorCobrar cuenta)
        {
            _context.Entry(cuenta).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cuenta = await _context.CuentasPorCobrar.FindAsync(id);
            if (cuenta != null)
            {
                _context.CuentasPorCobrar.Remove(cuenta);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<CuentaPorCobrar>> GetFilteredReportAsync(DateTime? fechaInicio, DateTime? fechaFin, string cliente)
        {
            var query = _context.CuentasPorCobrar.AsQueryable();

            if (fechaInicio.HasValue)
                query = query.Where(c => c.FechaDocumento >= fechaInicio.Value);

            if (fechaFin.HasValue)
                query = query.Where(c => c.FechaDocumento <= fechaFin.Value);

            if (!string.IsNullOrEmpty(cliente))
                query = query.Where(c => c.Cliente.Contains(cliente));

            return await query.ToListAsync();
        }
    }
}
