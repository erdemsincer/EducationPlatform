using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Domain.Entities
{
    public class Skill
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string SkillName { get; set; }
        public string ProficiencyLevel { get; set; }

        public  User User { get; set; }
    }
}
