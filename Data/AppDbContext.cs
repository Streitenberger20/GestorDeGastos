using GestorDeGastos.Models;
using Microsoft.EntityFrameworkCore;

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

    }

}
