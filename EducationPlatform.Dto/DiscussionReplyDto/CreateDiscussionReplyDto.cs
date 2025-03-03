using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Dto.DiscussionReplyDto
{
    public class CreateDiscussionReplyDto
    {
        public int DiscussionId { get; set; }  // Hangi tartışmaya ait
        public int UserId { get; set; }  // Cevaplayan kullanıcı ID'si
        public string Message { get; set; }
    }
}
