using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Dto.ReviewDto
{
    public class ResultReviewDto
    {
        public int Id { get; set; }
        public int InstructorId { get; set; }
        public string InstructorName { get; set; } // Hoca Adı (FullName)
        public int UserId { get; set; } // Değerlendiren kullanıcı
        public string UserName { get; set; } // Kullanıcının adı
        public int Rating { get; set; } // 1-5 arası puan
        public string Comment { get; set; } // Kullanıcının yorumu
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
