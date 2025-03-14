using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Domain.Entities
{
    public class Instructor
    {
        public int Id { get; set; }
        public string FullName { get; set; } // Ad Soyad
        public string Email { get; set; }
        public string Department { get; set; } // Bölüm
        public string Title { get; set; } // Unvan (Prof. Dr., Doç. Dr., Dr. Öğr. Üyesi vb.)
        public string Biography { get; set; } // Kısa açıklama
        public string ProfileImage { get; set; } // Profil fotoğrafı

        public ICollection<Review> Reviews { get; set; }
    }
}
