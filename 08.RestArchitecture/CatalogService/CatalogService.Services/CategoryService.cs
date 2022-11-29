using AutoMapper;
using CatalogService.Domain.DTOs.Category;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces.Repositories;
using CatalogService.Domain.Interfaces.Services;

namespace CatalogService.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICommonRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICommonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync(int pageSize, int pageNumber)
        {
            try
            {
                var categories = await _repository.Category.FindAllAsync(pageSize, pageNumber);

                var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categories);

                return categoryDtos;
            }
            catch
            {
                throw;
            }
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            try
            {
                var category = await _repository.Category.FindByIdAsync(id);

                var categoryDto = _mapper.Map<CategoryDto>(category);

                return categoryDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<CategoryDto> CreateCategoryAsync(CategoryForCreateDto categoryToCreate)
        {
            try
            {
                var categoryEntity = _mapper.Map<Category>(categoryToCreate);

                var createdEntity = _repository.Category.Create(categoryEntity);
                await _repository.SaveChangesAsync();

                var categoryDto = _mapper.Map<CategoryDto>(createdEntity);

                return categoryDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateCategoryAsync(CategoryForUpdateDto categoryToUpdate)
        {
            try
            {
                var categoryEntity = _mapper.Map<Category>(categoryToUpdate);

                _repository.Category.Update(categoryEntity);
                await _repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteCategoryAsync(int id)
        {
            try
            {
                _repository.Category.Delete(id);
                await _repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

    }
}
