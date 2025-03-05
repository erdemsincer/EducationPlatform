using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }  // Örn: Admin, User, Student, Teacher
        public ICollection<User> Users { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
