﻿using ManagementSystem.Domain.Entities;

namespace ManagementSystem.Infrastructure.Interfaces
{
    public interface ICuentaPorCobrarRepository
    {
        Task<List<CuentaPorCobrar>> GetAllAsync();
        Task<CuentaPorCobrar> GetByIdAsync(int id);
        Task AddAsync(CuentaPorCobrar cuenta);
        Task UpdateAsync(CuentaPorCobrar cuenta);
        Task DeleteAsync(int id);
        Task<List<CuentaPorCobrar>> GetFilteredReportAsync(DateTime? fechaInicio, DateTime? fechaFin, string cliente);
    }
}
