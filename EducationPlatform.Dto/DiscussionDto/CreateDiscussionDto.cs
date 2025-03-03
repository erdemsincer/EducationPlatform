using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Dto.DiscussionDto
{
    public class CreateDiscussionDto
    {
        public string Title { get; set; }  // Tartışma başlığı
        public string Content { get; set; }  // Tartışma açıklaması
        public int UserId { get; set; }
    }
}
