using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Domain.Entities
{
    public class Resource
    {
        public int Id { get; set; }  // Birincil anahtar
        public string Title { get; set; }  // Kaynak başlığı
        public string Description { get; set; }  // Açıklama
        public string FileUrl { get; set; }  // Dosya bağlantısı
        public int CategoryId { get; set; }  // Kategori ile ilişki
        public Category Category { get; set; }  // Navigasyon özelliği
        public int UserId { get; set; }  // Kaynağı ekleyen kullanıcı
        public User User { get; set; }  // Navigasyon özelliği
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
