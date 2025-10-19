using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using ServiceAbstraction;
using Shared.DataTransferObject.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _orderService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            return order is null ? NotFound() : Ok(order);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId) =>
            Ok(await _orderService.GetByUserIdAsync(userId));

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderDto dto) =>
            Ok(await _orderService.CreateAsync(dto));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _orderService.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }

        //[HttpGet("filter")]
        //public async Task<IActionResult> GetFiltered([FromQuery] PaginationParams parameters)
        //{
        //    var result = await _orderService.GetFilteredOrdersAsync(parameters);
        //    return Ok(result);
        //}
    }
}
