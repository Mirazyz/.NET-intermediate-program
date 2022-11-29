using CatalogService.Domain.DTOs.Item;
using CatalogService.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Api.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private const int PageSize = 30;

        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetAllItemsAsync(int? categoryId, int pageSize = PageSize, int pageNumber = 0)
        {
            IEnumerable<ItemDto> result;

            if (categoryId.HasValue)
            {
                result = await _itemService.GetAllItemsByCategoryIdAsync(categoryId.Value);
            }
            else
            {
                result = await _itemService.GetAllItemsAsync(pageSize, pageNumber);
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemByIdAsync(int id)
        {
            var item = await _itemService.GetItemByIdAsync(id);

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync([FromBody] ItemForCreateDto itemToCreate)
        {
            var createdItem = await _itemService.CreateItemAsync(itemToCreate);

            return Ok(createdItem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync([FromBody] ItemForUpdateDto itemToUpdate, int id)
        {
            if (itemToUpdate.Id != id)
            {
                return BadRequest($"Item id: {itemToUpdate.Id} does not match route id: {id}.");
            }

            await _itemService.UpdateItemAsync(itemToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(int id)
        {
            await _itemService.DeleteItemAsync(id);

            return NoContent();
        }
    }
}
