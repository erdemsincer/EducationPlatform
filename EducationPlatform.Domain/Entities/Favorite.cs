using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Domain.Entities
{
    public class Favorite
    {
        public int Id { get; set; }  // Birincil anahtar
        public int UserId { get; set; }  // Kullanıcı ID
        public User User { get; set; }  // Kullanıcı ile ilişki
        public int ResourceId { get; set; }  // Kaynak ID
        public Resource Resource { get; set; }  // Kaynak ile ilişki
    }
}
