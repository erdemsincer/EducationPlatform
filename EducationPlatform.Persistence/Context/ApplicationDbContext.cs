using Microsoft.EntityFrameworkCore;
using EducationPlatform.Domain.Entities;

namespace EducationPlatform.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Email'i benzersiz yapalım
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Favori sisteminde kullanıcı & kaynak ilişkisini tanımlayalım
            modelBuilder.Entity<Favorite>()
                .HasIndex(f => new { f.UserId, f.ResourceId })
                .IsUnique();
        }

        internal async Task<object> FindAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
