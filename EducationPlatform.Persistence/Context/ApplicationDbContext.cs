using Microsoft.EntityFrameworkCore;
using EducationPlatform.Domain.Entities;

namespace EducationPlatform.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // 📌 **Veritabanı Tabloları**
        public DbSet<User> Users { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<DiscussionReply> DiscussionReplies { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<CareerGoal> CareerGoals { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<CareerTest> CareerTests { get; set; }

        // 📌 **Kariyer Testi İçin Tablolar**
        public DbSet<CareerTestQuestion> CareerTestQuestions { get; set; } // **Sorular**
        public DbSet<CareerTestAnswer> CareerTestAnswers { get; set; } // **Kullanıcı Cevapları**

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ **Email benzersiz olacak**
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // ✅ **Favori sistemi için User + Resource tekil olacak**
            modelBuilder.Entity<Favorite>()
                .HasIndex(f => new { f.UserId, f.ResourceId })
                .IsUnique();

            // ✅ **UserRole Ara Tablo Yapısı**
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            // ✅ **CareerTestQuestion - JSON Formatında Şıkları Kaydetme**
            modelBuilder.Entity<CareerTestQuestion>()
                .Property(q => q.Options)
                .HasConversion(
                    v => string.Join(";", v),  // **Listeyi string olarak kaydet**
                    v => v.Split(";", System.StringSplitOptions.RemoveEmptyEntries).ToList()  // **Stringi listeye çevir**
                );

            // ✅ **Kullanıcının Her Soruyu Bir Kez Cevaplamasını Sağla**
            modelBuilder.Entity<CareerTestAnswer>()
                .HasIndex(a => new { a.UserId, a.QuestionId })
                .IsUnique();
        }
    }
}
