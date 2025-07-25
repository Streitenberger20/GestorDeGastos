

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace GestorDeGastos.Models
{
    public class Gasto
    {
        public int Id { get; set; }



        public DateTime FechaGasto { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El Importe debe ser mayor a 0.")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public decimal Importe { get; set; }

        public string Moneda { get; set; }

        public int UsuarioId { get; set; }
        [ValidateNever]
        public Usuario Usuario { get; set; }

        public int RubroId { get; set; }
        [ValidateNever]
        public Rubro Rubro { get; set; }


        public int DetalleId { get; set; }
        [ValidateNever]
        public Detalle Detalle { get; set; }

        public Gasto() { }
    }
}
