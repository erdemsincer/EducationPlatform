using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Dto.ResourceDto
{
    public class ResultResourceDto
    {
        public int Id { get; set; }  // Birincil anahtar
        public string Title { get; set; }  // Kaynak başlığı
        public string Description { get; set; }  // Açıklama
        public string FileUrl { get; set; }  // Dosya bağlantısı
        public int CategoryId { get; set; }  // Kategori ile ilişki
        public int UserId { get; set; }  // Kaynağı ekleyen kullanıcı
        public DateTime CreatedAt { get; set; }
        public string UserName { get; set; }  // Kullanıcının ismi
        public string CategoryName { get; set; }  // Kategorinin ismi
    }
}
