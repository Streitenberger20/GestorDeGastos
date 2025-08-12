using GestorDeGastos.Models;

namespace GestorDeGastos.ViewModels
{
    public class EditarRubroViewModel
    {
            public int Id { get; set; }
            public string NombreRubro { get; set; }

            public List<EditarDetalleViewModel> Detalles { get; set; } = new List<EditarDetalleViewModel>();

            public List<int> RolesSeleccionados { get; set; } = new List<int>();

            public List<Rol> RolesDisponibles { get; set; } = new List<Rol>();
    }
}
