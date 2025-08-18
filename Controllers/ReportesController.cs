using GestorDeGastos.Data;
using GestorDeGastos.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorDeGastos.Controllers
{
    [Authorize(Roles = "JEFE")]
    public class ReportesController : Controller
    {
        private readonly AppDbContext _context;

        public ReportesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ListadoGastos(DateTime? fechaDesde, DateTime? fechaHasta, int? usuarioId, int? rubroId, string moneda)
        {


            var query = _context.Gastos
                .Include(g => g.Usuario)
                .Include(g => g.Rubro)
                .Include(g => g.Detalle)
                .AsQueryable();

            // Aplicar filtros
            query = query.Where(g => g.FechaGasto >= fechaDesde && g.FechaGasto <= fechaHasta && g.Moneda == moneda);

            if (usuarioId.HasValue)
                query = query.Where(g => g.UsuarioId == usuarioId.Value);

            if (rubroId.HasValue)
                query = query.Where(g => g.RubroId == rubroId.Value);

            var resultados = await query.OrderByDescending(g => g.FechaGasto)
                .Select(g => new GastoDetalleViewModel
                {
                    NumeroGasto = g.Id,
                    EsActivo = g.esActivo,
                    Fecha = g.FechaGasto,
                    Usuario = g.Usuario.NombreUsuario,
                    Rubro = g.Rubro.NombreRubro,
                    Detalle = g.Detalle.NombreDetalle,
                    Monto = g.Importe,
                    Moneda = g.Moneda,
                    Comentario = g.Comentario
                })
                .ToListAsync();

            var vm = new GastoFiltroViewModel
            {
                FechaDesde = fechaDesde,
                FechaHasta = fechaHasta,
                UsuarioId = usuarioId,
                RubroId = rubroId,
                Usuarios = await _context.Usuarios
                .Where(u => u.esActivo)
                .OrderBy(u => u.NombreUsuario)
                .ToListAsync(),
                Rubros = await _context.Rubros
                .Where(r => r.esActivo)
                .OrderBy(r => r.NombreRubro)
                .ToListAsync(),
                Resultados = resultados,
                Total = resultados.Sum(r => r.Monto),
                Moneda = moneda
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> ExportarExcel(DateTime? fechaDesde, DateTime? fechaHasta, int? usuarioId, int? rubroId, string moneda)
        {
            var query = _context.Gastos
                .Include(g => g.Usuario)
                .Include(g => g.Rubro)
                .Include(g => g.Detalle)
                .AsQueryable();

            if (fechaDesde.HasValue)
                query = query.Where(g => g.FechaGasto >= fechaDesde.Value);

            if (fechaHasta.HasValue)
                query = query.Where(g => g.FechaGasto <= fechaHasta.Value);

            if (usuarioId.HasValue)
                query = query.Where(g => g.UsuarioId == usuarioId.Value);

            if (rubroId.HasValue)
                query = query.Where(g => g.RubroId == rubroId.Value);

            if (!string.IsNullOrEmpty(moneda))
                query = query.Where(g => g.Moneda == moneda);

            var lista = await query
                .OrderByDescending(g => g.FechaGasto)
                .ToListAsync();

            using var workbook = new ClosedXML.Excel.XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Gastos");

            // Encabezados
            worksheet.Cell(1, 1).Value = "Fecha";
            worksheet.Cell(1, 2).Value = "Usuario";
            worksheet.Cell(1, 3).Value = "Rubro";
            worksheet.Cell(1, 4).Value = "Detalle";
            worksheet.Cell(1, 5).Value = "Monto";
            worksheet.Cell(1, 6).Value = "Moneda";

            // Cargar filas
            for (int i = 0; i < lista.Count; i++)
            {
                var g = lista[i];
                worksheet.Cell(i + 2, 1).Value = g.FechaGasto.ToString("yyyy-MM-dd");
                worksheet.Cell(i + 2, 2).Value = g.Usuario.NombreUsuario;
                worksheet.Cell(i + 2, 3).Value = g.Rubro.NombreRubro;
                worksheet.Cell(i + 2, 4).Value = g.Detalle.NombreDetalle;
                worksheet.Cell(i + 2, 5).Value = g.Importe;
                worksheet.Cell(i + 2, 6).Value = g.Moneda;
            }

            // Formato autoajustado
            worksheet.Columns().AdjustToContents();

            // Devolver archivo
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReportesGastos-" + DateTime.Now.ToString() + ".xlsx");
        }
    }
}
