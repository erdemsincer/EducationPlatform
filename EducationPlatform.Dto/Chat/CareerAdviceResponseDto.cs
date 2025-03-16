using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Dto.Chat
{
    public class CareerAdviceResponseDto
    {
        [JsonProperty("career_advice")] // API'nin döndürdüğü isimle eşleşmesini sağlıyoruz
        public string CareerAdvice { get; set; }
    }
}
