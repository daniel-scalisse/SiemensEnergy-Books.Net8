using Books_Business.Modules.Authors;
using Books_Business.Modules.Books;
using Books_Business.Modules.Genders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Books_Data.Context
{
    public class BooksDbContext : DbContext
    {
        public BooksDbContext(DbContextOptions<BooksDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Author> Authores { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookImage> BookImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(50)");

            //Searches the DBContext for all mapped entities to relate to the classes that inherit from IEntityTypeConfiguration.
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BooksDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries().Where(e => e.Entity.GetType().GetProperty("InclusionDate") != null))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Property("InclusionDate").CurrentValue = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Property("InclusionDate").IsModified = false;
                        break;
                }
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}