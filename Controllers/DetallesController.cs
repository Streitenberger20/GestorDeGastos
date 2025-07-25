using GestorDeGastos.Data;
using GestorDeGastos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace GestorDeGastos.Controllers
{
    public class DetallesController : Controller
    {
        private readonly AppDbContext _context;

        public DetallesController(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> CrearDetalle(int id )
        {

            ViewBag.RubroId = id;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearDetalle(Detalle detalle)
        { 
            _context.Detalles.Add(detalle);
            await _context.SaveChangesAsync();
            return RedirectToAction("DetallesRubro", "Rubros", new {id = detalle.RubroId});
        }

        public async Task<IActionResult> EditarDetalle(int id)
        {
            var detalle = await _context.Detalles.FindAsync(id);
            if (detalle == null) return NotFound();

            ViewBag.Rubros = new SelectList(await _context.Rubros.ToListAsync(), "Id", "NombreRubro", detalle.RubroId);
            return View(detalle);
        }

        [HttpPost]
        public async Task<IActionResult> EditarDetalle(Detalle detalle)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Rubros = new SelectList(await _context.Rubros.ToListAsync(), "Id", "NombreRubro", detalle.RubroId);
                return View(detalle);
            }

            _context.Update(detalle);
            await _context.SaveChangesAsync();
            return RedirectToAction("ListadoUsuarios");
        }

        public async Task<IActionResult> EliminarDetalle(int id)
        {
            var detalle = await _context.Detalles.FindAsync(id);
            if (detalle == null) return NotFound();

            _context.Detalles.Remove(detalle);
            await _context.SaveChangesAsync();
            return RedirectToAction("ListadoUsuarios");
        }


    }
}
