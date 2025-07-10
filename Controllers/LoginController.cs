using GestorDeGastos.Data;
using Microsoft.AspNetCore.Mvc;

namespace GestorDeGastos.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext db;

        public LoginController(AppDbContext context)
        {
            db = context;
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(string nombreDeUsuario, string contraseña)
        {
            var usuario = db.Usuario.FirstOrDefault(u => u.NombreDeUsuario == nombreDeUsuario && u.Contraseña == contraseña);
            if (usuario != null)
            {
                HttpContext.Session.SetString("Usuario", usuario.NombreDeUsuario);
                HttpContext.Session.SetString("Id", usuario.Id.ToString());
                HttpContext.Session.SetString("Rol", usuario.RolId.ToString());

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Usuario o clave incorrecta";
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
