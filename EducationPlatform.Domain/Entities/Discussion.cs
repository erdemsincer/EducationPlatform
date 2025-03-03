using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Domain.Entities
{
    public class Discussion
    {
        public int Id { get; set; }
        public string Title { get; set; }  // Tartışma başlığı
        public string Content { get; set; }  // Tartışma açıklaması
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int UserId { get; set; }  // Tartışmayı açan kişi
        public User User { get; set; }  // Navigation property
        public ICollection<DiscussionReply> Replies { get; set; }  // Cevaplar
    }
}
