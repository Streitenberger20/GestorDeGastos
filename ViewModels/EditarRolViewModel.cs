using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestorDeGastos.ViewModels
{
    public class EditarRolViewModel
    {
        public int RolId { get; set; }
        public string NombreRol { get; set; }

        // Rubros disponibles en la app
        public List<SelectListItem> RubrosDisponibles { get; set; } = new();

        // IDs de rubros marcados (activos)
        public List<int> RubrosSeleccionados { get; set; } = new();
    }
}
