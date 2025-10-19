using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _paymentService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _paymentService.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("ByOrder/{orderId}")]
        public async Task<IActionResult> GetByOrderId(int orderId) =>
            Ok(await _paymentService.GetByOrderIdAsync(orderId));

        [HttpGet("ByMethod/{method}")]
        public async Task<IActionResult> GetByMethod(string method) =>
            Ok(await _paymentService.GetByMethodAsync(method));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePaymentDto dto)
        {
            var result = await _paymentService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _paymentService.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }


    }
}
