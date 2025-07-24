
using GestorDeGastos.DTOs;

namespace GestorDeGastos.ViewModels
{
    public class DashboardViewModel
    {
        public decimal TotalARS { get; set; }
        public decimal TotalUSD { get; set; }
        public int TotalGastos { get; set; }
        public decimal PromedioPorEmpleado { get; set; }
        public decimal GastoMaximo { get; set; }

        public List<GastosPorMesDTO> GastosUltimosTresMeses { get; set; }
        public List<RubroPorcentajeDTO> RubrosMasUsados { get; set; }
        public List<UsuarioGastoDTO> GastosPorUsuario { get; set; }
    }
}
