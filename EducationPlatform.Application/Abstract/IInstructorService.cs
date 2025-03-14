using EducationPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Application.Abstract
{
    public interface IInstructorService:IGenericService<Instructor>
    {
        Task<Instructor> GetInstructorWithReviewsAsync(int instructorId);
        Task<List<Instructor>> GetLastFourInstructorsAsync();
    }
}
