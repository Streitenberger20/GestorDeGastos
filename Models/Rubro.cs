namespace GestorDeGastos.Models
{
    public class Rubro
    {
        public int Id { get; set; }
        public string NombreRubro { get; set; }

        public ICollection<Descripcion> Descripciones { get; set; }
        public ICollection<Gasto> Gastos { get; set; }
        public ICollection<RolRubro> RolRubros { get; set; }

        public Rubro() { }
    }
}
