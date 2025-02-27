using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Dto.FavoriteDto
{
    public class CreateFavoriteDto
    {
        public int UserId { get; set; }  // Kullanıcı ID
        // Kullanıcı ile ilişki
        public int ResourceId { get; set; }  // Kaynak ID
       
    }
}
