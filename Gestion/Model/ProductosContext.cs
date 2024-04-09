using Microsoft.EntityFrameworkCore;

namespace Gestion.Model
{
    public class ProductosContext : DbContext
    {
        public ProductosContext(DbContextOptions<ProductosContext>options):base(options) { 
        
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Producto>().HasIndex(c=>c.nombre).IsUnique();
            modelBuilder.Entity<Usuario>().HasIndex(c => c.nombre).IsUnique();
        }

      
    }
}
