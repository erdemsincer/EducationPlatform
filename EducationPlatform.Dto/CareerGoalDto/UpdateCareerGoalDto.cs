using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Dto.CareerGoalDto
{
    public class UpdateCareerGoalDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string GoalName { get; set; }
        public string Description { get; set; }
    }
}
