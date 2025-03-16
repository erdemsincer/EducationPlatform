namespace EducationPlatform.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }  // Otomatik artan birincil anahtar
        public string FullName { get; set; }  // Kullanıcının adı ve soyadı
        public string Email { get; set; }  // Kullanıcı e-posta adresi (Benzersiz olmalı)
        public string PasswordHash { get; set; }  // Şifre (hashlenmiş)
        public string ProfileImage { get; set; }  // Profil fotoğrafı URL
        public string? Role { get; set; } = "User";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // Kullanıcı oluşturulma tarihi

        // **İlişkiler**
        public ICollection<Discussion> Discussions { get; set; }
        public ICollection<DiscussionReply> DiscussionReplies { get; set; }
        public ICollection<Role> Roles { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Review> Reviews { get; set; }

        // **Chatbot İçin Yeni Eklenenler**
        public ICollection<Interest> Interests { get; set; }  // Kullanıcının ilgi alanları
        public ICollection<Skill> Skills { get; set; }  // Kullanıcının becerileri
        public ICollection<CareerGoal> CareerGoals { get; set; }  // Kullanıcının kariyer hedefleri
        public ICollection<CareerTest> CareerTests { get; set; }  // Kullanıcının kariyer hedefleri
    }
}
