using GestorDeGastos.Data;
using GestorDeGastos.Models;
using GestorDeGastos.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GestorDeGastos.Controllers
{
    public class RubrosController : Controller
    {
        private readonly AppDbContext _context;

        public RubrosController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult ListadoRubros()
        {
            var rubros = _context.Rubros.ToList();
            return View(rubros);
        }

        [HttpGet]
        public IActionResult CrearRubro()
        {
            var vm = new RubroViewModel
            {
                RolesDisponibles = _context.Roles
                    .Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.NombreRol })
                    .ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult CrearRubro(RubroViewModel vm)
        {
            if (string.IsNullOrWhiteSpace(vm.NombreRubro) || !vm.RolesSeleccionados.Any())
            {
                ModelState.AddModelError("", "Debe ingresar un nombre y seleccionar al menos un rol.");

                vm.RolesDisponibles = _context.Roles
                    .Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.NombreRol })
                    .ToList();

                return View(vm);
            }

            var rubro = new Rubro { NombreRubro = vm.NombreRubro };
            _context.Rubros.Add(rubro);
            _context.SaveChanges();

            // Asociaciones con roles seleccionados
            foreach (var rolId in vm.RolesSeleccionados)
            {
                _context.RolRubros.Add(new RolRubro
                {
                    RubroId = rubro.Id,
                    RolId = rolId
                });
            }

            _context.SaveChanges();

            return RedirectToAction("ListadoRubros");
        }

        [HttpGet]
        public IActionResult DetallesRubro(int id)
        {

            Rubro rubro = _context.Rubros
                .Include(r => r.Detalles)
                .FirstOrDefault(r => r.Id == id);

            return View(rubro);

        }
    }
}
