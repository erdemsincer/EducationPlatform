using EducationPlatform.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IChatbotDal
{
    Task<User> GetUserWithDetailsAsync(int userId);
}
