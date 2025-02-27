using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Dto.CategoryDto
{
    public class UpdateCategoryDto
    {
        public int Id { get; set; }  // Birincil anahtar
        public string Name { get; set; }
    }
}
