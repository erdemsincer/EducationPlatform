using System;
using System.Linq;
using System.Threading.Tasks;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Context;
using Microsoft.EntityFrameworkCore;

public class EFChatbotDal : IChatbotDal
{
    private readonly ApplicationDbContext _context;

    public EFChatbotDal(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserWithDetailsAsync(int userId)
    {
        return await _context.Users
            .Include(u => u.Skills)
            .Include(u => u.Interests)
            .Include(u => u.CareerGoals)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }
}
