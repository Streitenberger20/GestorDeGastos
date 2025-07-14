namespace GestorDeGastos.Models
{
    public class Descripcion
    {
        public int Id { get; set; }
        public string NombreDescripcion { get; set; }

        public int RubroId { get; set; }
        public Rubro Rubro { get; set; }

        public Descripcion() { }
    }
}
