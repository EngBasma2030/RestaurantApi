using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using ServiceAbstraction;
using Shared.OrderItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _orderItemService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _orderItemService.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderItemDto dto)
        {
            var created = await _orderItemService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateOrderItemDto dto)
        {
            var updated = await _orderItemService.UpdateAsync(id, dto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _orderItemService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }

        // دوال اضافية 
        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetByOrderId(int orderId) =>
            Ok(await _orderItemService.GetByOrderIdAsync(orderId));

        [HttpGet("search")]
        public async Task<IActionResult> SearchByMenuItem([FromQuery] string name) =>
            Ok(await _orderItemService.SearchByMenuItemNameAsync(name));

        [HttpGet("filter")]
        public async Task<IActionResult> FilterByPrice([FromQuery] decimal min, [FromQuery] decimal max) =>
            Ok(await _orderItemService.FilterByPriceRangeAsync(min, max));

        [HttpGet("total/{orderId}")]
        public async Task<IActionResult> CalculateTotal(int orderId) =>
            Ok(await _orderItemService.CalculateOrderTotalAsync(orderId));
    }
}
