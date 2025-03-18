using ManagementSystem.Application.Interfaces;
using ManagementSystem.Domain.Entities;
using ManagementSystem.Infrastructure.Interfaces;


namespace ManagementSystem.Application.Services
{
    public class CuentaPorCobrarService : ICuentaPorCobrarService
    {
        private readonly ICuentaPorCobrarRepository _repository;

        public CuentaPorCobrarService(ICuentaPorCobrarRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CuentaPorCobrar>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<CuentaPorCobrar> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(CuentaPorCobrar cuenta)
        {
            await _repository.AddAsync(cuenta);
        }

        public async Task UpdateAsync(CuentaPorCobrar cuenta)
        {
            await _repository.UpdateAsync(cuenta);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<List<CuentaPorCobrar>> GetFilteredReportAsync(DateTime? fechaInicio, DateTime? fechaFin, string cliente)
        {
            return await _repository.GetFilteredReportAsync(fechaInicio, fechaFin, cliente);
        }

        public async Task<byte[]> ExportToExcelAsync(List<CuentaPorCobrar> cuentas)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Reporte");
                worksheet.Cells.LoadFromCollection(cuentas, true);

                return package.GetAsByteArray();
            }
        }
    }
}
