using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }  // Birincil anahtar
        public string Content { get; set; }  // Yorum içeriği
        public int UserId { get; set; }  // Yorumu yazan kullanıcı
        public User User { get; set; }  // Navigasyon
        public int ResourceId { get; set; }  // Hangi kaynağa ait
        public Resource Resource { get; set; }  // Navigasyon
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
