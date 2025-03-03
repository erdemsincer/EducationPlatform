using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Domain.Entities
{
    public class DiscussionReply
    {
        public int Id { get; set; }
        public string Message { get; set; }  // Cevap içeriği
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int DiscussionId { get; set; }  // Hangi tartışmaya ait
        public Discussion Discussion { get; set; }
        public int UserId { get; set; }  // Kim cevapladı
        public User User { get; set; }
    }
}
