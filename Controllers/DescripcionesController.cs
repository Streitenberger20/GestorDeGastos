using GestorDeGastos.Data;
using GestorDeGastos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GestorDeGastos.Controllers
{
    public class DescripcionesController : Controller
    {
        private readonly AppDbContext _context;

        public DescripcionesController(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> CrearDescripcion()
        {


            ViewBag.Rubros = new SelectList(await _context.Rubros.ToListAsync(), "Id", "NombreRubro");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearDescripcion(Descripcion descripcion)
        { 
            _context.Descripciones.Add(descripcion);
            await _context.SaveChangesAsync();
            return RedirectToAction("CrearDescripcion");
        }

        public async Task<IActionResult> EditarDescripcion(int id)
        {
            var descripcion = await _context.Descripciones.FindAsync(id);
            if (descripcion == null) return NotFound();

            ViewBag.Rubros = new SelectList(await _context.Rubros.ToListAsync(), "Id", "NombreRubro", descripcion.RubroId);
            return View(descripcion);
        }

        [HttpPost]
        public async Task<IActionResult> EditarDescripcion(Descripcion descripcion)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Rubros = new SelectList(await _context.Rubros.ToListAsync(), "Id", "NombreRubro", descripcion.RubroId);
                return View(descripcion);
            }

            _context.Update(descripcion);
            await _context.SaveChangesAsync();
            return RedirectToAction("ListadoUsuarios");
        }

        public async Task<IActionResult> EliminarDescripcion(int id)
        {
            var descripcion = await _context.Descripciones.FindAsync(id);
            if (descripcion == null) return NotFound();

            _context.Descripciones.Remove(descripcion);
            await _context.SaveChangesAsync();
            return RedirectToAction("ListadoUsuarios");
        }


    }
}
