using ErrorlineSystem.DataContext.Dtos;
using ErrorlineSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace ErrorlineSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EquipmentOrderController : ControllerBase
    {
        private readonly IEquipmentOrderService _orderService;
        public EquipmentOrderController(IEquipmentOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] EquipmentOrderCreateDto orderDto)
        {
            /*
            var order = await _orderService.CreateOrderAsync(orderDto);
            return CreatedAtAction(nameof(TrackOrder), new { orderId = order.Id }, order);
            */
            var result = await _orderService.CreateOrderAsync(orderDto);
            return Ok(result);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> TrackOrder(int orderId)
        {
            var order = await _orderService.TrackOrderAsync(orderId);
            return Ok(order);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var order = await _orderService.GetAllOrdersAsync();
            return Ok(order);
        }
    }
}
