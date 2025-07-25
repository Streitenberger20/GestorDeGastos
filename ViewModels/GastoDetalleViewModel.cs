namespace GestorDeGastos.ViewModels
{
    public class GastoDetalleViewModel
    {
        public string Usuario { get; set; }
        public string Rubro { get; set; }
        public string Detalle { get; set; } 
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }

        public string Moneda { get; set; }
    }
}
