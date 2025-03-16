using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Dto.CareerTest
{
    public class CareerTestAnswersDto
    {
        public int UserId { get; set; }
        public Dictionary<int, string> Answers { get; set; }
    }
}
