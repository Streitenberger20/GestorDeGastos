using DocumentFormat.OpenXml.InkML;
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
            var rubros = _context.Rubros.Where(r => r.esActivo).ToList();
            return View(rubros);
        }

        [HttpGet]
        public IActionResult CrearRubro()
        {
            var roles = _context.Roles.ToList();

            ViewBag.Roles = roles;

            return View();
        }

        [HttpPost]
        public IActionResult Crear(CrearRubroViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var roles = _context.Roles.ToList();
                ViewBag.Roles = roles;
                return View(model);
            }

            var rubro = new Rubro
            {
                NombreRubro = model.NombreRubro,
                esActivo = true,
                Detalles = model.DetallesDescripcion
                    .Where(d => !string.IsNullOrWhiteSpace(d))
                    .Select(d => new Detalle { NombreDetalle = d })
                    .ToList()
            };

            // Agregamos los roles con relación muchos a muchos con Rubro
            rubro.RolRubros = _context.Roles
                .Where(r => model.RolesSeleccionados.Contains(r.Id))
                .Select(r => new RolRubro { RolId = r.Id, Rubro = rubro })
                .ToList();

            _context.Rubros.Add(rubro);
            _context.SaveChanges();

            return RedirectToAction("ListadoRubros");
        }

        // GET: Rubro/Editar/5
        public IActionResult Editar(int id)
        {
            var rubro = _context.Rubros
                .Include(r => r.Detalles.Where(d => d.esActivo))
                .Include(r => r.RolRubros.Where(rr => rr.EsActivo))
                .ThenInclude(rr => rr.Rol)
                .FirstOrDefault(r => r.Id == id);

            if (rubro == null) return NotFound();

            var model = new EditarRubroViewModel
            {
                Id = rubro.Id,
                NombreRubro = rubro.NombreRubro,
                Detalles = rubro.Detalles.Select(d => new EditarDetalleViewModel
                {
                    Id = d.Id,
                    Descripcion = d.NombreDetalle
                }).ToList(),
                RolesSeleccionados = rubro.RolRubros.Select(rr => rr.RolId).ToList(),
                RolesDisponibles = _context.Roles.ToList()
            };

            return View(model);
        }

        // POST: Rubro/Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(EditarRubroViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.RolesDisponibles = _context.Roles.ToList();
                return View(model);
            }

            var rubro = _context.Rubros
                .Include(r => r.Detalles)
                .Include(r => r.RolRubros)
                .FirstOrDefault(r => r.Id == model.Id);

            if (rubro == null) return NotFound();

            // Actualizar nombre
            rubro.NombreRubro = model.NombreRubro;

            // --- DETALLES ---
            var idsEnviados = model.Detalles.Select(d => d.Id).Where(id => id > 0).ToList();

            // Marcar como inactivos los que no fueron enviados
            foreach (var detalle in rubro.Detalles)
            {
                if (!idsEnviados.Contains(detalle.Id))
                {
                    detalle.esActivo = false;
                }
            }

            // Actualizar detalles existentes
            foreach (var detalleEdit in model.Detalles.Where(d => d.Id > 0))
            {
                var detalle = rubro.Detalles.FirstOrDefault(d => d.Id == detalleEdit.Id);
                if (detalle != null)
                {
                    detalle.NombreDetalle = detalleEdit.Descripcion;
                    detalle.esActivo = true; // reactivarlo por si estaba inactivo
                }
            }

            // Agregar nuevos detalles
            var detallesNuevos = model.Detalles
                .Where(d => d.Id == 0 && !string.IsNullOrWhiteSpace(d.Descripcion))
                .ToList();

            foreach (var nuevoDetalle in detallesNuevos)
            {
                rubro.Detalles.Add(new Detalle
                {
                    NombreDetalle = nuevoDetalle.Descripcion,
                    esActivo = true
                });
            }

            // --- ROLES ---
            // Marcar como inactivos todos los actuales
            foreach (var rr in rubro.RolRubros)
            {
                rr.EsActivo = false;
            }

            // Activar o agregar roles seleccionados
            if (model.RolesSeleccionados != null && model.RolesSeleccionados.Any())
            {
                foreach (var rolId in model.RolesSeleccionados.Distinct())
                {
                    var existente = rubro.RolRubros.FirstOrDefault(rr => rr.RolId == rolId);

                    if (existente != null)
                    {
                        existente.EsActivo = true;
                    }
                    else
                    {
                        rubro.RolRubros.Add(new RolRubro
                        {
                            RolId = rolId,
                            RubroId = rubro.Id,
                            EsActivo = true
                        });
                    }
                }
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
