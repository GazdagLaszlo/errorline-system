using ErrorlineSystem.DataContext.Context;
using ErrorlineSystem.DataContext.Dtos;
using ErrorlineSystem.DataContext.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using static ErrorlineSystem.DataContext.Entities.EquipmentOrder;


namespace ErrorlineSystem.Services;

public interface IEquipmentOrderService
{
    Task<EquipmentOrderDto> CreateOrderAsync(EquipmentOrderCreateDto equipmentOrderCreateDto);
    Task<EquipmentOrderDto> TrackOrderAsync(int orderId);
    Task<IList<EquipmentOrderDto>> GetAllOrdersAsync();
}

public class EquipmentOrderService(AppDbContext context, IMapper mapper) : IEquipmentOrderService
{
    public async Task<EquipmentOrderDto> CreateOrderAsync(EquipmentOrderCreateDto equipmentOrderCreateDto)
    {
        var issue = await context.Issues.FindAsync(equipmentOrderCreateDto.IssueId);
        if (issue == null)
        {
            throw new Exception($"Issue not found with Id: {equipmentOrderCreateDto.IssueId}!");
        }
        var equipment = await context.Equipments.FindAsync(equipmentOrderCreateDto.EquipmentId);
        if (equipment == null)
        {
            throw new Exception($"Equipment not found with Id: {equipmentOrderCreateDto.EquipmentId}!");
        }        

        var order = new EquipmentOrder
        {
            Issue = issue,
            Equipment = equipment,
            Quantity = equipmentOrderCreateDto.Quantity,
            State = EquipmentOrderState.Open,
        };

        await context.EquipmentOrders.AddAsync(order);
        await context.SaveChangesAsync();

        return mapper.Map<EquipmentOrderDto>(order);
    }
    
    public async Task<EquipmentOrderDto> TrackOrderAsync(int orderId)
    {
        var order = await context.EquipmentOrders
            .FirstOrDefaultAsync(x => x.Id == orderId);

        if (order == null)
        {
            throw new KeyNotFoundException($"Order not found with Id: {orderId}");
        }
        return mapper.Map<EquipmentOrderDto>(order);
        
    }

    public async Task<IList<EquipmentOrderDto>> GetAllOrdersAsync()
    {
        var orders = await context.EquipmentOrders            
            .Include(o => o.Issue)
            .Include(o => o.Equipment)
            .ToListAsync();

        return mapper.Map<IList<EquipmentOrderDto>>(orders);
    }

}