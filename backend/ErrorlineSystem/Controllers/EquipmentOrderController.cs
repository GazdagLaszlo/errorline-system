using ErrorlineSystem.DataContext.Dtos;
using ErrorlineSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErrorlineSystem.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize]
public class EquipmentOrderController(IEquipmentOrderService orderService) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = "MaintenanceManager,MaintenanceWorker,Administrator")]
    [ProducesResponseType<EquipmentOrderResponseDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateOrder([FromBody] EquipmentOrderCreateDto orderDto)
    {
        var result = await orderService.CreateOrderAsync(orderDto);
        return Ok(result);
    }

    [HttpGet("{orderId}")]
    [Authorize(Roles = "MaintenanceManager")]
    [ProducesResponseType<EquipmentOrderResponseDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> TrackOrder(int orderId)
    {
        var order = await orderService.TrackOrderAsync(orderId);
        return Ok(order);
    }

    [HttpGet]
    [Authorize(Roles = "MaintenanceManager,Administrator")]
    [ProducesResponseType<IList<EquipmentOrderResponseDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllOrders()
    {
        var order = await orderService.GetAllOrdersAsync();
        return Ok(order);
    }
}