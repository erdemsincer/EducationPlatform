using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Dto.CareerGoalDto
{
    public class ResultCareerGoalDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string GoalName { get; set; }
        public string Description { get; set; }
    }
}
