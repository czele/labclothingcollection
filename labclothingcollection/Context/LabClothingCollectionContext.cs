using labclothingcollection.Models;
using labclothingcollection.Seeds;
using Microsoft.EntityFrameworkCore;

namespace labclothingcollection.Context
{
    public class LabClothingCollectionContext : DbContext
    {
        public LabClothingCollectionContext(DbContextOptions options) : base(options) { }

        public LabClothingCollectionContext( ) { }

        public virtual DbSet<Usuario> Usuario { get; set; }

        public virtual DbSet<Modelo> Modelo { get; set; }

        public virtual DbSet<Colecao> Colecao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasData(MockUsuarios.usuario);
            modelBuilder.Entity<Colecao>().HasData(MockColecao.colecao);
            modelBuilder.Entity<Modelo>().HasData(MockModelo.modelo);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("ServerConnection");
            }
        }
    }
}
