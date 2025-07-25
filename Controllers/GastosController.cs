using GestorDeGastos.Data;
using GestorDeGastos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GestorDeGastos.Controllers
{
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
                .Include(r => r.RolRubros)
                    .ThenInclude(rr => rr.Rubro)
                .FirstOrDefault(r => r.NombreRol == rolNombre);

            var rubros = rol.RolRubros.Select(rr => rr.Rubro).ToList();
            ViewBag.Rubros = new SelectList(rubros, "Id", "NombreRubro");

            Gasto gasto = new Gasto
            {
                FechaGasto = DateTime.Now,
            };

            return View(gasto);
        }

        [HttpGet]
        public JsonResult ObtenerDetalles(int rubroId)
        {
            var detalles = _context.Detalles
                .Where(d => d.RubroId == rubroId)
                .Select(d => new { d.Id, d.NombreDetalle})
                .ToList();

            return Json(detalles);
        }

        [HttpPost]
        public IActionResult RegistrarGasto(Gasto gasto)
        {

            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value ?? "0");
            gasto.UsuarioId = usuarioId;

            if (!ModelState.IsValid)
            {
                var rolNombre = User.FindFirst(ClaimTypes.Role)?.Value;
                var rol = _context.Roles
                .Include(r => r.RolRubros)
                .ThenInclude(rr => rr.Rubro)
                .FirstOrDefault(r => r.NombreRol == rolNombre);
                var rubros = rol.RolRubros.Select(rr => rr.Rubro).ToList();
                ViewBag.Rubros = new SelectList(rubros, "Id", "NombreRubro");
                return View(gasto);
            }

            _context.Gastos.Add(gasto);
            _context.SaveChanges();

            return RedirectToAction("RegistrarGasto");
        }
    }
}
