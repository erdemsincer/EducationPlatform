using EducationPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Application.Abstract
{
    public interface IReviewService:IGenericService<Review>
    {
        Task<List<Review>> GetReviewsByInstructorIdAsync(int instructorId);
    }
}
