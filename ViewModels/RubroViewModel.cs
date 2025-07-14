using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestorDeGastos.ViewModels
{
    public class RubroViewModel
    {
        public string NombreRubro { get; set; }

        public List<int> RolesSeleccionados { get; set; } = new List<int>();

        public List<SelectListItem> RolesDisponibles { get; set; } = new List<SelectListItem>();
    }
}
