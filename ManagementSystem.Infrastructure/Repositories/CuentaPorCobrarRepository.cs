using ManagementSystem.Domain.Entities;
using ManagementSystem.Infrastructure.Data;
using ManagementSystem.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace ManagementSystem.Infrastructure.Repositories
{
    public class CuentaPorCobrarRepository : ICuentaPorCobrarRepository
    {
        private readonly ApplicationDbContext _context;

        ///////////////Método que lee archivo de excel
        //private readonly string _excelFilePath = "C:\\ProjectsDEV\\ManagementSystem\\ManagementSystem.Infrastructure\\Repositories\\data.xlsx";

        public CuentaPorCobrarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        ///////////////Método que lee archivo de excel
        //public async Task<List<CuentaPorCobrar>> GetAllAsync()
        //{
        //    var cuentas = new List<CuentaPorCobrar>();

        //    try
        //    {
        //        using (var package = new ExcelPackage(new FileInfo(_excelFilePath)))
        //        {
        //            // Validar si hay hojas
        //            if (package.Workbook.Worksheets.Count == 0)
        //            {
        //                throw new Exception("El archivo Excel no contiene hojas.");
        //            }

        //            // Acceder a la primera hoja
        //            var worksheet = package.Workbook.Worksheets[0];

        //            // Validar si la hoja tiene datos
        //            if (worksheet.Dimension == null || worksheet.Dimension.Rows < 2)
        //            {
        //                throw new Exception("La hoja está vacía o no tiene suficientes filas.");
        //            }

        //            // Procesar filas
        //            for (int row = 2; row <= worksheet.Dimension.Rows; row++)
        //            {
        //                try
        //                {
        //                    var cuenta = new CuentaPorCobrar
        //                    {
        //                        Id = int.Parse(worksheet.Cells[row, 1].Text), // Columna A (si existe un ID)
        //                        FechaDocumento = DateTime.Parse(worksheet.Cells[row, 2].Text), // Columna B
        //                        MesAnio = worksheet.Cells[row, 3].Text, // Columna C
        //                        Tipo = worksheet.Cells[row, 4].Text, // Columna D
        //                        Documento = worksheet.Cells[row, 5].Text, // Columna E
        //                        Cliente = worksheet.Cells[row, 6].Text, // Columna F
        //                        Valor = decimal.Parse(worksheet.Cells[row, 7].Text), // Columna G
        //                        NetoR = decimal.Parse(worksheet.Cells[row, 8].Text), // Columna H
        //                        Cartera = decimal.Parse(worksheet.Cells[row, 9].Text), // Columna I
        //                        NetoF = decimal.Parse(worksheet.Cells[row, 10].Text), // Columna J
        //                        IVA = decimal.Parse(worksheet.Cells[row, 11].Text), // Columna K
        //                        Pagado = decimal.Parse(worksheet.Cells[row, 12].Text), // Columna L
        //                        FechaPago = DateTime.Parse(worksheet.Cells[row, 13].Text), // Columna M
        //                        Dias = int.Parse(worksheet.Cells[row, 14].Text), // Columna N
        //                        EfectivoOtroMeses = decimal.Parse(worksheet.Cells[row, 15].Text), // Columna O
        //                        Colombia = decimal.Parse(worksheet.Cells[row, 16].Text), // Columna P
        //                        Davivienda = decimal.Parse(worksheet.Cells[row, 17].Text), // Columna Q
        //                        Bancolombia = decimal.Parse(worksheet.Cells[row, 18].Text), // Columna R
        //                        Efectivo = decimal.Parse(worksheet.Cells[row, 19].Text) // Columna S
        //                    };
        //                    cuentas.Add(cuenta);
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine($"Error al procesar fila {row}: {ex.Message}");
        //                    // Opcional: continuar con las siguientes filas o romper el ciclo
        //                }
        //            }
        //        }

        //        return cuentas;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error al leer el archivo Excel: {ex.Message}");
        //        return new List<CuentaPorCobrar>();  // Retorna una lista vacía en caso de fallo
        //    }
        //}




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
