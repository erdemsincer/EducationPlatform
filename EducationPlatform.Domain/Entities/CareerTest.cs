using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Domain.Entities
{
    public class CareerTest
    {
        public int Id { get; set; }
        public int UserId { get; set; }  // Kullanıcı testi çözüyor
        public string Question { get; set; } // Testin sorusu
        public List<string> Options { get; set; } // Şıklar
        public string SelectedAnswer { get; set; } // Kullanıcının seçtiği yanıt
        public User User { get; set; }
    }
}
