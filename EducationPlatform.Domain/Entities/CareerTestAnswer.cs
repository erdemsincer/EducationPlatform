using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Domain.Entities
{
    public class CareerTestAnswer
    {
        [Key]
        public int Id { get; set; } // Cevap ID
        public int UserId { get; set; } // Kullanıcı ID
        public int QuestionId { get; set; } // Hangi soruya ait?
        public string SelectedAnswer { get; set; } // Kullanıcının seçtiği şık

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("QuestionId")]
        public CareerTestQuestion Question { get; set; }
    }
}
