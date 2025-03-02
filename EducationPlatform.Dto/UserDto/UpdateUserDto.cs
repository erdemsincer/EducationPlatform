using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Dto.UserDto
{
    public class UpdateUserDto
    {
        public int Id { get; set; }  // Otomatik artan birincil anahtar
        public string FullName { get; set; }  // Kullanıcının adı ve soyadı
        public string Email { get; set; }  // Kullanıcı e-posta adresi (Benzersiz olmalı)
        public string ProfileImage { get; set; }  // Profil fotoğrafı URL
        public string? PasswordHash { get; set; }  // Şifre (hashlenmiş)
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // Kullanıcı oluşturulma tarihi
    }
}
