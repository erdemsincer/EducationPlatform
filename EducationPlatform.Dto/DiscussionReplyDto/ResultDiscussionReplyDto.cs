using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Dto.DiscussionReplyDto
{
    public class ResultDiscussionReplyDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string UserFullName { get; set; }  // Cevaplayan kullanıcının adı
        public DateTime CreatedAt { get; set; }
    }
}
