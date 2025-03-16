using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Domain.Entities
{
    public class CareerTestQuestion
    {
        [Key]
        public int Id { get; set; } // Soru ID
        public string Question { get; set; } // Test sorusu
        public List<string> Options { get; set; } // Seçenekler (JSON olarak saklanabilir)
    }
}
