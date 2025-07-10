namespace GestorDeGastos.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public int RolId { get; set; }
        public Rol Rol { get; set; }

        public Categoria() { }
    }
}
