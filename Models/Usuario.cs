using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorDeGastos.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string NombreDeUsuario { get; set; }

        public string Contraseña { get; set; }

        public string Rol {  get; set; }

        public List<Gasto> Gastos { get; set; }

        public Usuario() { }
    }
}
