using Exercises.DataLayer.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Exercises.DataLayer
{
    public class ExercisesDbContext: DbContext
    {
        public ExercisesDbContext(DbContextOptions<ExercisesDbContext> options)
            : base(options)
        {   
        }

        public virtual DbSet<Product> products { get; set; }

        public virtual DbSet<ShopperHistory> shopperHistories { get; set; }

        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                        .HasKey(p => p.name);

            modelBuilder.Entity<ShopperHistory>()
                        .HasKey(sh => new { sh.customerId, sh.productName });
        }
        #endregion
    }
}




