﻿using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.ResourceDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPlatform.Persistence.Abstract
{
    public interface IResourceDal : IGenericDal<Resource>
    {
        Task<List<Resource>> GetByCategoryIdAsync(int categoryId);
        Task<List<ResultResourceDto>> GetResourceDetailsAsync();
    }
}
