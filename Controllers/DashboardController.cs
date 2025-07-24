using GestorDeGastos.Data;
using GestorDeGastos.DTOs;
using GestorDeGastos.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestorDeGastos.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var hoy = DateTime.Today;
            var gastos = await _context.Gastos
                .Include(g => g.Usuario)
                .Include(g => g.Rubro)
                .Where(g => g.FechaGasto >= hoy.AddMonths(-3))
                .ToListAsync();

            var mesActual = hoy.Month;
            var añoActual = hoy.Year;
            var gastosDelMes = gastos.Where(g => g.FechaGasto.Month == mesActual && g.FechaGasto.Year == añoActual).ToList();

            var usuarios = gastosDelMes.Select(g => g.UsuarioId).Distinct().Count();

            var model = new DashboardViewModel
            {
                TotalARS = gastosDelMes.Where(g => g.Moneda == "AR$").Sum(g => g.Importe),
                TotalUSD = gastosDelMes.Where(g => g.Moneda == "USD").Sum(g => g.Importe),
                TotalGastos = gastosDelMes.Count,
                PromedioPorEmpleado = usuarios > 0 ? gastosDelMes.Sum(g => g.Importe) / usuarios : 0,
                GastoMaximo = gastosDelMes.Any() ? gastosDelMes.Max(g => g.Importe) : 0,
                GastosUltimosTresMeses = gastos
                    .GroupBy(g => new { g.FechaGasto.Month, g.FechaGasto.Year })
                    .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                    .Select(g => new GastosPorMesDTO
                    {
                        Mes = $"{g.Key.Month}/{g.Key.Year}",
                        TotalARS = g.Where(x => x.Moneda == "AR$").Sum(x => x.Importe),
                        TotalUSD = g.Where(x => x.Moneda == "USD").Sum(x => x.Importe)
                    }).ToList(),

                RubrosMasUsados = gastosDelMes
                    .GroupBy(g => g.Rubro.NombreRubro)
                    .Select(g => new RubroPorcentajeDTO
                    {
                        Rubro = g.Key,
                        Porcentaje = Math.Round((decimal)g.Count() * 100 / gastosDelMes.Count, 1)
                    }).ToList(),

                GastosPorUsuario = gastosDelMes
                    .GroupBy(g => g.Usuario.NombreUsuario)
                    .Select(g => new UsuarioGastoDTO
                    {
                        Usuario = g.Key,
                        Total = g.Sum(x => x.Importe)
                    }).ToList()
            };

            return View(model);
        }

        public IActionResult ExportarPDF()
        {
            // Implementaremos esto en el siguiente paso
            return RedirectToAction("Index");
        }
    }
}
