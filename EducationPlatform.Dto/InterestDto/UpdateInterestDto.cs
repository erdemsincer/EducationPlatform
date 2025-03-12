using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Dto.InterestDto
{
    public class UpdateInterestDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string InterestName { get; set; }

        
    }
}
