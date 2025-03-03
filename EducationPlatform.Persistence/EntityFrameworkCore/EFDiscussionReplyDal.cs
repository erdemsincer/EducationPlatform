using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using EducationPlatform.Persistence.Context;
using EducationPlatform.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Persistence.EntityFrameworkCore
{
    public class EFDiscussionReplyDal : GenericRepository<Discussion>, IDiscussionDal
    {
        public EFDiscussionReplyDal(ApplicationDbContext context) : base(context)
        {
        }
    }
}
