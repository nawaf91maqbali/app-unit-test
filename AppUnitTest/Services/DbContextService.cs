using AppUnitTest.Models;
using Microsoft.EntityFrameworkCore;

namespace AppUnitTest.Services
{
    public class DbContextService : DbContext
    {
        public DbContextService(DbContextOptions<DbContextService> options) : base(options)
        {
        }

        /// <summary>
        /// Configures the model for the database context, including entity configurations and relationships.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure your entities here
            // For example, modelBuilder.Entity<User>().ToTable("Users");
        }

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            var result = base.SaveChanges();

            // Optionally, you can log the changes or perform other actions here

            ChangeTracker.Clear(); // Clear the change tracker to avoid memory leaks and stale data

            return result;
        }

        /// <summary>
        /// Asynchronously saves all changes made in this context to the database.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = base.SaveChangesAsync(cancellationToken);

            // Optionally, you can log the changes or perform other actions here

            ChangeTracker.Clear(); // Clear the change tracker to avoid memory leaks and stale data

            
            return  result;
        }

        // Add DbSet properties for your entities
        public DbSet<User> Users { get; set; }
    }
}
