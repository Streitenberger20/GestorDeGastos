using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GestorDeGastos.Models
{
    public class Gasto
    {
        public int Id { get; set; }

        public DateOnly FechaGasto { get; set; } 

        public string Comentario { get; set; }

        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; }

        public double Importe { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public Gasto() { }
    }
}
