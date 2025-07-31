
using System.ComponentModel.DataAnnotations;

namespace GestorDeGastos.ViewModels
{
    public class GastoViewModel
    {
        [Required(ErrorMessage = "Campo obligatorio")]
        public DateTime FechaGasto { get; set; } = DateTime.Now;

        [Range(0.01, double.MaxValue, ErrorMessage = "El Importe debe ser mayor a 0.")]
        [Required(ErrorMessage = "Campo obligatorio")]
        public decimal? Importe { get; set; }

        public string Moneda { get; set; }

        public int UsuarioId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un rubro.")]
        public int RubroId { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un detalle.")]
        public int DetalleId { get; set; }


        public GastoViewModel() { }

    }
}
