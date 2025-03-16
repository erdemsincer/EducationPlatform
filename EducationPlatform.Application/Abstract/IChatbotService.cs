using System.Threading.Tasks;

public interface IChatbotService
{
    Task<string> GetCareerAdviceAsync(int userId);  // Kullanıcının genel bilgilerinden kariyer önerisi alır
    Task<string> GetCareerAdviceFromTestAsync(int userId, string formattedAnswers);  // Test cevaplarına göre kariyer önerisi alır
}
