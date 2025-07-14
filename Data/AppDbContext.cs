using Microsoft.EntityFrameworkCore;
using GestorDeGastos.Models;

namespace GestorDeGastos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Gasto> Gastos { get; set; }
        public DbSet<Rubro> Rubros { get; set; }
        public DbSet<Descripcion> Descripciones { get; set; }
        public DbSet<RolRubro> RolRubros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 🔹 Tabla intermedia RolRubro (muchos a muchos)
            modelBuilder.Entity<RolRubro>()
                .HasKey(rr => new { rr.RolId, rr.RubroId });

            modelBuilder.Entity<RolRubro>()
                .HasOne(rr => rr.Rol)
                .WithMany(r => r.RolRubros)
                .HasForeignKey(rr => rr.RolId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RolRubro>()
                .HasOne(rr => rr.Rubro)
                .WithMany(r => r.RolRubros)
                .HasForeignKey(rr => rr.RubroId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔹 Rubro → Descripciones (1:N)
            modelBuilder.Entity<Rubro>()
                .HasMany(r => r.Descripciones)
                .WithOne(d => d.Rubro)
                .HasForeignKey(d => d.RubroId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔹 Usuario → Gastos (1:N)
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Gastos)
                .WithOne(g => g.Usuario)
                .HasForeignKey(g => g.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔹 Gasto → Rubro (N:1)
            modelBuilder.Entity<Gasto>()
                .HasOne(g => g.Rubro)
                .WithMany()
                .HasForeignKey(g => g.RubroId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔹 Gasto → Descripcion (N:1)
            modelBuilder.Entity<Gasto>()
                .HasOne(g => g.Descripcion)
                .WithMany()
                .HasForeignKey(g => g.DescripcionId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔹 Gasto → Usuario (N:1) ya definido más arriba, pero aquí por claridad
            modelBuilder.Entity<Gasto>()
                .HasOne(g => g.Usuario)
                .WithMany(u => u.Gastos)
                .HasForeignKey(g => g.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
