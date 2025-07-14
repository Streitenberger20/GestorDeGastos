using GestorDeGastos.Data;
using GestorDeGastos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;
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

            if (rol == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var rubros = rol.RolRubros.Select(rr => rr.Rubro).ToList();
            ViewBag.Rubros = new SelectList(rubros, "Id", "NombreRubro");

            return View();
        }

        [HttpGet]
        public JsonResult ObtenerDescripciones(int rubroId)
        {
            var descripciones = _context.Descripciones
                .Where(d => d.RubroId == rubroId)
                .Select(d => new { d.Id, d.NombreDescripcion })
                .ToList();

            return Json(descripciones);
        }

        [HttpPost]
        public IActionResult RegistrarGasto(Gasto gasto)
        {

            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value ?? "0");
            gasto.UsuarioId = usuarioId;

            _context.Gastos.Add(gasto);
            _context.SaveChanges();

            return RedirectToAction("RegistrarGasto");
        }
    }
}
