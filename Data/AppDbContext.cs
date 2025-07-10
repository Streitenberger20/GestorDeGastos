using Microsoft.EntityFrameworkCore;
using GestorDeGastos.Models;

namespace GestorDeGastos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
             : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Gasto> Gasto { get; set; }
        public DbSet<Categoria> Categoria { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación Usuario → Gasto
            modelBuilder.Entity<Gasto>()
                .HasOne(g => g.Usuario)
                .WithMany(u => u.Gastos)
                .HasForeignKey(g => g.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);  // o .NoAction

            // Relación Gasto → Categoria
            modelBuilder.Entity<Gasto>()
                .HasOne(g => g.Categoria)
                .WithMany() // porque en Categoria no tienes lista de Gastos
                .HasForeignKey(g => g.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);  // opcional

            // Relación Usuario → Rol
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany() // porque en Rol no tienes lista de Usuarios
                .HasForeignKey(u => u.RolId)
                .OnDelete(DeleteBehavior.Restrict);  // opcional
        }
        public DbSet<GestorDeGastos.Models.Rol> Rol { get; set; } = default!;
    }
}
