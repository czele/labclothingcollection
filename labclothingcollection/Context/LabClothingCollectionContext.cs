using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace labclothingcollection.Context
{
    public class LabClothingCollectionContext : DbContext
    {
        public LabClothingCollectionContext(DbContextOptions options) : base(options) { }

        public LabClothingCollectionContext( ) { }

        //public virtual DbSet< >
    }
}
