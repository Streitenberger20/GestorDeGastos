using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GestorDeGastos.Models
{
    public class Gasto
    {
        public int Id { get; set; }
        public DateTime FechaGasto { get; set; }
        public decimal Importe { get; set; }
        public string Moneda { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int RubroId { get; set; }
        public Rubro Rubro { get; set; }

        public int DescripcionId { get; set; }
        public Descripcion Descripcion { get; set; }

        public Gasto() { }
    }
}
