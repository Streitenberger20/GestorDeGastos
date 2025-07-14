namespace GestorDeGastos.Models
{
    public class RolRubro
    {
        public int RolId { get; set; }
        public Rol Rol { get; set; }

        public int RubroId { get; set; }
        public Rubro Rubro { get; set; }

        public RolRubro() { }
    }
}
