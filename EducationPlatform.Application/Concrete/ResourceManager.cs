using AutoMapper;
using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.ResourceDto;
using EducationPlatform.Persistence.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPlatform.Application.Concrete
{
    public class ResourceManager : IResourceService
    {
        private readonly IResourceDal _resourceDal;
        private readonly IMapper _mapper;

        public ResourceManager(IResourceDal resourceDal, IMapper mapper)
        {
            _resourceDal = resourceDal;
            _mapper = mapper;
        }

        public async Task<List<Resource>> GetByCategoryIdAsync(int categoryId)
        {
            return await _resourceDal.GetByCategoryIdAsync(categoryId);
        }

        public async Task<List<ResultResourceDto>> GetResourceDetailsAsync()
        {
            return await _resourceDal.GetResourceDetailsAsync();
        }

        public async Task<List<ResultResourceDto>> GetResourcesByUserIdAsync(int userId)
        {
            var resources = await _resourceDal.GetResourcesByUserIdAsync(userId);
            return _mapper.Map<List<ResultResourceDto>>(resources);

        }

        public async Task TAddAsync(Resource entity)
        {
            await _resourceDal.AddAsync(entity);
        }

        public async Task TDeleteAsync(Resource entity)
        {
            await _resourceDal.DeleteAsync(entity);
        }

        public async Task<Resource> TGetByIdAsync(int id)
        {
            return await _resourceDal.GetByIdAsync(id);
        }

        public async Task<List<Resource>> TGetListAllAsync()
        {
            return await _resourceDal.GetListAllAsync();
        }

        public async Task TUpdateAsync(Resource entity)
        {
            await _resourceDal.UpdateAsync(entity);
        }
        public async Task<List<Resource>> GetResourcesByCategoryWithUser(int categoryId)
        {
            return await _resourceDal.GetResourcesByCategoryWithUser(categoryId);
        }
        public async Task<List<Resource>> GetLatestResources()
        {
            return await _resourceDal.GetLatestResources();
        }

        public async Task<ResultResourceDto> GetResourceByIdAsync(int id)
        {
            return await _resourceDal.GetResourceByIdAsync(id);
        }
    }
}
