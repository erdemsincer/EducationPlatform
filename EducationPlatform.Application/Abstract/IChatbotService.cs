using System.Threading.Tasks;

public interface IChatbotService
{
    Task<string> GetCareerAdviceAsync(int userId);
}
