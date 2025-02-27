using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }  // Birincil anahtar
        public string Name { get; set; }  // Kategori adı
        public ICollection<Resource> Resources { get; set; }
    }
}
