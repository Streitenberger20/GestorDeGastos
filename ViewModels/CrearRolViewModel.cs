using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestorDeGastos.ViewModels
{
    public class CrearRolViewModel
    {
        public string NombreRol { get; set; }
        public List<int> RubrosSeleccionados { get; set; } = new();
        public List<SelectListItem> RubrosDisponibles { get; set; } = new();
    }
}
