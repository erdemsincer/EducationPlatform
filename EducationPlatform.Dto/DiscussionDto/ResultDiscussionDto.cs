using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Dto.DiscussionDto
{
    public class ResultDiscussionDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserFullName { get; set; }  // Kullanıcı adı (User'dan çekilecek)
        public DateTime CreatedAt { get; set; }
        public int ReplyCount { get; set; }
    }
}
