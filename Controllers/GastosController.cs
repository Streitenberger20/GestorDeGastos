
using GestorDeGastos.Data;
using GestorDeGastos.Models;
using GestorDeGastos.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GestorDeGastos.Controllers
{
    [Authorize]
    public class GastosController : Controller
    {
        private readonly AppDbContext _context;

        public GastosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult RegistrarGasto()
        {
            var rolNombre = User.FindFirst(ClaimTypes.Role)?.Value;

            var rol = _context.Roles
                .Include(r => r.RolRubros.Where(rr => rr.EsActivo))
                    .ThenInclude(rr => rr.Rubro)
                    .ThenInclude(r => r.Detalles.Where(d => d.esActivo))
                .FirstOrDefault(r => r.NombreRol == rolNombre);

            var rubros = rol?.RolRubros.Select(rr => rr.Rubro).Where(rr => rr.Detalles.Count != 0).ToList() ?? new List<Rubro>();

            int primerRubroId = rubros.First().Id;

            var detalles = _context.Detalles
                .Where(d => d.RubroId == primerRubroId)
                .ToList();

            ViewBag.Rubros = new SelectList(rubros, "Id", "NombreRubro", primerRubroId);
            ViewBag.Detalles = new SelectList(detalles, "Id", "NombreDetalle");
            ViewBag.UltimosGastos = _context.Gastos
                .Where(g => g.UsuarioId == int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value ?? "0" ) && g.esActivo && g.Importe>0)
                .Include(g => g.Rubro)
                .Include(g => g.Detalle)
                .OrderByDescending(g => g.Id)
                .Take(5)
                .ToList();

            var gastoVM = new GastoViewModel
            {
                RubroId = primerRubroId,
                DetalleId = detalles.FirstOrDefault()?.Id ?? 0
            };

            return View(gastoVM);
        }

        [HttpGet]
        public JsonResult ObtenerDetalles(int rubroId)
        {
            var detalles = _context.Detalles
                .Where(d => d.RubroId == rubroId && d.esActivo)
                .Select(d => new { d.Id, d.NombreDetalle})
                .ToList();

            return Json(detalles);
        }

        [HttpPost]
        public IActionResult RegistrarGasto(GastoViewModel gastoVM)
        {
            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (!ModelState.IsValid)
            {
                var rolNombre = User.FindFirst(ClaimTypes.Role)?.Value;
                var rol = _context.Roles
                    .Include(r => r.RolRubros.Where(rr => rr.EsActivo))
                        .ThenInclude(rr => rr.Rubro)
                        .ThenInclude(r => r.Detalles.Where(d => d.esActivo))
                    .FirstOrDefault(r => r.NombreRol == rolNombre);
                var rubros = rol?.RolRubros.Select(rr => rr.Rubro).Where(rr => rr.Detalles.Count != 0).ToList() ?? new List<Rubro>();
                ViewBag.Rubros = new SelectList(rubros, "Id", "NombreRubro");
                ViewBag.UltimosGastos = _context.Gastos
                .Where(g => g.UsuarioId == usuarioId && g.esActivo && g.Importe>0)
                .Include(g => g.Rubro)
                .Include(g => g.Detalle)
                .OrderByDescending(g => g.Id)
                .Take(5)
                .ToList();

                return View(gastoVM);
            }
            var gasto = new Gasto
            {
                UsuarioId = usuarioId,
                FechaGasto = gastoVM.FechaGasto,
                Importe = gastoVM.Importe.Value,
                Moneda = gastoVM.Moneda,
                DetalleId = gastoVM.DetalleId,
                RubroId = gastoVM.RubroId,
                Comentario = gastoVM.Comentario,
            };

            _context.Gastos.Add(gasto);
            _context.SaveChanges();

            return RedirectToAction("RegistrarGasto");
        }

        [HttpPost]
        public IActionResult EliminarGasto(int id)
        {
            var gasto = _context.Gastos.Find(id);
            if (gasto == null) return NotFound();
            gasto.esActivo = false;
            gasto.Comentario = "Gasto eliminado: " + gasto.Id;

            var credito = new Gasto
            {
                UsuarioId = gasto.UsuarioId,
                FechaGasto = gasto.FechaGasto,
                Importe = -gasto.Importe,
                Moneda = gasto.Moneda,
                DetalleId = gasto.DetalleId,
                RubroId = gasto.RubroId,
                Comentario = "Credito eliminación de gasto: " + gasto.Id,
            };

            _context.Gastos.Add(credito);

            _context.SaveChanges();
            return RedirectToAction("RegistrarGasto");
        }
    }
}
