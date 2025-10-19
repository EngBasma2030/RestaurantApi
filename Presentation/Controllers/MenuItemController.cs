using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ServiceAbstraction;
using Shared.DataTransferObject.MenuItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;
        private readonly IMapper _mapper;

        public MenuItemController(IMenuItemService menuItemService, IMapper mapper)
        {
            _menuItemService = menuItemService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _menuItemService.GetAllAsync();
            var mapped = _mapper.Map<IEnumerable<MenuItemDto>>(items);
            return Ok(mapped);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _menuItemService.GetByIdAsync(id);
            if (item == null)
                return NotFound("Item not found.");

            var mapped = _mapper.Map<MenuItemDto>(item);
            return Ok(mapped);
        }

        [HttpPost]
        //public async Task<IActionResult> Add([FromBody] CreateMenuItemDto dto)
        //{
        //    var menuItem = _mapper.Map<MenuItem>(dto);
        //    var created = await _menuItemService.AddAsync(menuItem);
        //    var result = _mapper.Map<MenuItemDto>(created);
        //    return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMenuItemDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch.");

            var menuItem = _mapper.Map<MenuItem>(dto);
            await _menuItemService.UpdateAsync(menuItem);
            return Ok("Updated successfully.");
        }

        [HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    await _menuItemService.DeleteAsync(id);
        //    return Ok("Deleted successfully.");
        //}

        // البحث والفلترة
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string name)
        {
            var items = await _menuItemService.SearchByNameAsync(name);
            var mapped = _mapper.Map<IEnumerable<MenuItemDto>>(items);
            return Ok(mapped);
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetByCategory(string category)
        {
            var items = await _menuItemService.FilterByCategoryAsync(category);
            var mapped = _mapper.Map<IEnumerable<MenuItemDto>>(items);
            return Ok(mapped);
        }

        [HttpGet("available/{isAvailable}")]
        public async Task<IActionResult> GetByAvailability(bool isAvailable)
        {
            var items = await _menuItemService.FilterByAvailabilityAsync(isAvailable);
            var mapped = _mapper.Map<IEnumerable<MenuItemDto>>(items);
            return Ok(mapped);
        }

        [HttpGet("price-range")]
        public async Task<IActionResult> GetByPriceRange([FromQuery] decimal min, [FromQuery] decimal max)
        {
            var items = await _menuItemService.FilterByPriceRangeAsync(min, max);
            var mapped = _mapper.Map<IEnumerable<MenuItemDto>>(items);
            return Ok(mapped);
        }
    }


}
