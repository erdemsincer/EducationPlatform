using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Dto.UserDto
{
    public class ResultUserDto
    {
        public int Id { get; set; }  // Otomatik artan birincil anahtar
        public string FullName { get; set; }  // Kullanıcının adı ve soyadı
        public string Email { get; set; }  // Kullanıcı e-posta adresi (Benzersiz olmalı)
        public string PasswordHash { get; set; }  // Şifre (hashlenmiş)
        public string ProfileImage { get; set; }  // Profil fotoğrafı URL
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // Kullanıcı oluşturulma tarihi
    }
}
