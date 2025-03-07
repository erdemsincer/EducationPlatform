using EducationPlatform.Dto.DiscussionReplyDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Dto.DiscussionDto
{
    public class DiscussionWithRepliesDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserFullName { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ResultDiscussionReplyDto> Replies { get; set; }
    }
}
