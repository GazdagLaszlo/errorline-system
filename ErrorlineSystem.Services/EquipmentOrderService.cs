using ErrorlineSystem.DataContext.Context;
using ErrorlineSystem.DataContext.Dtos;
using ErrorlineSystem.DataContext.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;


namespace ErrorlineSystem.Services;

public interface IEquipmentOrderService
{
    Task<EquipmentOrderResponseDto> CreateOrderAsync(EquipmentOrderCreateDto equipmentOrderCreateDto);
    Task<EquipmentOrderResponseDto> TrackOrderAsync(int orderId);
    Task<IList<EquipmentOrderResponseDto>> GetAllOrdersAsync();
}

public class EquipmentOrderService(AppDbContext context, IMapper mapper) : IEquipmentOrderService
{
    public async Task<EquipmentOrderResponseDto> CreateOrderAsync(EquipmentOrderCreateDto equipmentOrderCreateDto)
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

        var order = mapper.Map<EquipmentOrder>(equipmentOrderCreateDto);
        order.Issue = issue;
        order.Equipment = equipment;

        await context.EquipmentOrders.AddAsync(order);
        await context.SaveChangesAsync();

        return mapper.Map<EquipmentOrderResponseDto>(order);
    }
    
    public async Task<EquipmentOrderResponseDto> TrackOrderAsync(int orderId)
    {
        var order = await context.EquipmentOrders
            .FirstOrDefaultAsync(x => x.Id == orderId);

        if (order == null)
        {
            throw new KeyNotFoundException($"Order not found with Id: {orderId}");
        }

        context.Entry(@order).Reference(x => x.Issue).Load();
        context.Entry(@order).Reference(x => x.Equipment).Load();

        return mapper.Map<EquipmentOrderResponseDto>(order);
    }

    public async Task<IList<EquipmentOrderResponseDto>> GetAllOrdersAsync()
    {
        var orders = await context.EquipmentOrders
            .Include(x=>x.Issue)
            .Include(x => x.Equipment)
            .ToListAsync();

        return mapper.Map<IList<EquipmentOrderResponseDto>>(orders);
    }

}