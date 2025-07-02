namespace GestorDeGastos.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string NombreDeUsuario { get; set; }

        public HashCode Contraseña { get; set; }

        public string Tipo { get; set; }

        public List<Gasto> Gastos { get; set; }

        public Usuario() { }
    }
}
