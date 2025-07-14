using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorDeGastos.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }

        public int RolId { get; set; }
        public Rol Rol { get; set; }

        public ICollection<Gasto> Gastos { get; set; }

        public Usuario() { }
    }
}
