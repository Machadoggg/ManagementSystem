using ManagementSystem.Application.Interfaces;
using ManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasPorCobrarController : ControllerBase
    {
        private readonly ICuentaPorCobrarService _service;

        public CuentasPorCobrarController(ICuentaPorCobrarService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cuentas = await _service.GetAllAsync();
            return Ok(cuentas);
            ///////////////Método que lee archivo de excel
            //var cuentas = await _service.GetAllAsync();
            //var excelBytes = await _service.ExportToExcelAsync(cuentas);
            //return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reporte.xlsx");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cuenta = await _service.GetByIdAsync(id);
            if (cuenta == null) return NotFound();
            return Ok(cuenta);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CuentaPorCobrar cuenta)
        {
            await _service.AddAsync(cuenta);
            return CreatedAtAction(nameof(GetById), new { id = cuenta.Id }, cuenta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CuentaPorCobrar cuenta)
        {
            if (id != cuenta.Id) return BadRequest();
            await _service.UpdateAsync(cuenta);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("report")]
        public async Task<IActionResult> GetReport([FromQuery] DateTime? fechaInicio, [FromQuery] DateTime? fechaFin, [FromQuery] string cliente)
        {
            var cuentas = await _service.GetFilteredReportAsync(fechaInicio, fechaFin, cliente);
            return Ok(cuentas);
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportToExcel([FromQuery] DateTime? fechaInicio, [FromQuery] DateTime? fechaFin, [FromQuery] string cliente)
        {
            var cuentas = await _service.GetFilteredReportAsync(fechaInicio, fechaFin, cliente);
            var excelBytes = await _service.ExportToExcelAsync(cuentas);
            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reporte.xlsx");
        }

        [HttpGet("exportTotal")]
        public async Task<IActionResult> ExportTotalToExcel()
        {
            var cuentas = await _service.GetAllAsync();
            var excelBytes = await _service.ExportToExcelAsync(cuentas);
            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteTotal.xlsx");
        }

    }
}
