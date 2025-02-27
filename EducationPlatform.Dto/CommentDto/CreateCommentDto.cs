using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Dto.CommentDto
{
    public class CreateCommentDto
    {
        public string Content { get; set; }  // Yorum içeriği
        public int UserId { get; set; }  // Yorumu yazan kullanıcı
                                         // Navigasyon
        public int ResourceId { get; set; }  // Hangi kaynağa ait
        // Navigasyon
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
