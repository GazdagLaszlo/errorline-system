using ErrorlineSystem.DataContext.Dtos;
using ErrorlineSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErrorlineSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    //[Authorize]
    public class EquipmentOrderController : ControllerBase
    {
        private readonly IEquipmentOrderService _orderService;
        public EquipmentOrderController(IEquipmentOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        //[Authorize(Roles = "MaintenanceManager,MaintenanceWorker")]
        public async Task<IActionResult> CreateOrder([FromBody] EquipmentOrderCreateDto orderDto)
        {
            var result = await _orderService.CreateOrderAsync(orderDto);
            return Ok(result);
        }

        [HttpGet("{orderId}")]
        //[Authorize(Roles = "MaintenanceManager)]
        public async Task<IActionResult> TrackOrder(int orderId)
        {
            var order = await _orderService.TrackOrderAsync(orderId);
            return Ok(order);
        }

        [HttpGet]
        //[Authorize(Roles = "MaintenanceManager)]
        public async Task<IActionResult> GetAllOrders()
        {
            var order = await _orderService.GetAllOrdersAsync();
            return Ok(order);
        }
    }
}
