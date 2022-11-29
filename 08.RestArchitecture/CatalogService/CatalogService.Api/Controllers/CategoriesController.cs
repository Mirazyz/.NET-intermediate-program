using AutoMapper;
using CatalogService.Domain.DTOs.Category;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace CatalogService.Api.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private const int pageSize = 30;
        
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategoriesAsync(int pageSize = pageSize, int pageNumber = 0)
        {
            var categories = await _categoryService.GetAllCategoriesAsync(pageSize, pageNumber);

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CategoryForCreateDto categoryToCreate)
        {
            var createdCategory = await _categoryService.CreateCategoryAsync(categoryToCreate);

            return Ok(createdCategory);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategoryAsync([FromBody] CategoryForUpdateDto categoryToUpdate, int id)
        {
            if(categoryToUpdate.Id != id)
            {
                return BadRequest($"Category id: {categoryToUpdate.Id} does not match route id: {id}.");
            }

            await _categoryService.UpdateCategoryAsync(categoryToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategoryAsync(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            
            return NoContent();
        }
    }
}
