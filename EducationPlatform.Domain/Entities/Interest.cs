using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Domain.Entities
{
    public class Interest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string InterestName { get; set; }

        public  User User { get; set; }
    }
}
