using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Dto.SkillDto
{
    public class CreateSkillDto
    {
        public int UserId { get; set; }
        public string SkillName { get; set; }
        public string ProficiencyLevel { get; set; }
    }
}
