namespace GestorDeGastos.Models
{
    public class Gasto
    {
        public int Id { get; set; }

        public DateOnly FechaGasto { get; set; }

        public string Detalle { get; set; }

        public string Categoria { get; set; }

        public int UsuarioID { get; set; }
        public Usuario Usuario { get; set; }

        public Gasto() { }
    }
}
