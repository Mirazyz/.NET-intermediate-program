using CatalogService.Domain.DTOs.Category;

namespace CatalogService.Domain.Interfaces.Services
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync(int pageSize, int pageNumber);
        public Task<CategoryDto> GetCategoryByIdAsync(int id);
        public Task<CategoryDto> CreateCategoryAsync(CategoryForCreateDto categoryToCreate);
        public Task UpdateCategoryAsync(CategoryForUpdateDto categoryToUpdate);
        public Task DeleteCategoryAsync(int id);
    }
}
