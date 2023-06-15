using labclothingcollection.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace labclothingcollection.Context
{
    public class LabClothingCollectionContext : DbContext
    {
        public LabClothingCollectionContext(DbContextOptions options) : base(options) { }

        public LabClothingCollectionContext( ) { }

        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Modelo> Modelo { get; set; }
        public virtual DbSet<Colecao> Colecao { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("ServerConnection");
            }
        }
    }
}
