namespace GestorDeGastos.ViewModels
{
    public class CrearRubroViewModel
    {
        public string NombreRubro { get; set; }

        public List<string> DetallesDescripcion { get; set; } = new List<string>();
        public List<int> RolesSeleccionados { get; set; } = new List<int>();
    }
}
