using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Dto.CommentDto
{
    public class ResultCommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ResourceId { get; set; }
        public string ResourceTitle { get; set; } // Kaynağın adını göstermek için
    }
}
