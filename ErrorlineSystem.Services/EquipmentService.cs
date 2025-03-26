using ErrorlineSystem.DataContext.Context;
using ErrorlineSystem.DataContext.Dtos;
using ErrorlineSystem.DataContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace ErrorlineSystem.Services;

public interface IEquipmentService
{
    /// <summary>
    /// Lekéri az összes Equipment-elemet.
    /// </summary>
    Task<IEnumerable<EquipmentSearchListItemDto>> GetAllAsync();

    /// <summary>
    /// Lekér egy Equipment-elemet az azonosító alapján.
    /// </summary>
    Task<EquipmentDto> GetByIdAsync(int id);

    
    /// <summary>
    /// Létrehoz egy új Equipment-elemet.
    /// </summary>
    Task<int> CreateAsync(EquipmentCreateDto equipmentCreateDto);
    
    /// <summary>
    /// Frissít egy meglévő Equipment-elemet.
    /// </summary>
    Task UpdateAsync(int id, EquipmentCreateDto equipmentUpdateDto);
    
    /// <summary>
    /// Töröl egy Equipment-elemet az azonosító alapján.
    /// </summary>
    Task DeleteAsync(int id);
}

public class EquipmentService(AppDbContext context) : IEquipmentService
{
    public async Task<IEnumerable<EquipmentSearchListItemDto>> GetAllAsync()
    {
        return await context.Equipments
            .Include(e => e.Facility)
            .Select(equipment => new EquipmentSearchListItemDto()
            {
                Id = equipment.Id,
                Name = equipment.Name,
                IsInStock = equipment.IsInStock,
                Quantity = equipment.Quantity,
                IssueId = equipment.Issue.Id,
                FacilityId = equipment.Facility.Id,
                CreateBy = equipment.CreateBy,
                CreateDateTime = equipment.CreateDateTime,
            }).ToListAsync();
    }

    public async Task<EquipmentDto> GetByIdAsync(int id)
    {
        var @equipment = await context.Equipments.FindAsync(id);

        if (@equipment == null)
        {
            throw new Exception("Equipment with the given id not found");
        }
        
        context.Entry(@equipment).Reference(e => e.Facility).Load();
        context.Entry(@equipment).Reference(e => e.Issue).Load();
        context.Entry(@equipment).Collection(e => e.EquipmentOrders).Load();

        return new EquipmentDto()
        {
            Id = @equipment.Id,
            Name = @equipment.Name,
            IsInStock = @equipment.IsInStock,
            Quantity = equipment.Quantity,
            IssueId = @equipment.Issue.Id,
            Facility = new FacilityDto()
            {
                Id = @equipment.Facility.Id,
                Name = @equipment.Facility.Name,
            },
            CreateBy = @equipment.CreateBy,
            CreateDateTime = @equipment.CreateDateTime,
        };
    }

    public async Task<int> CreateAsync(EquipmentCreateDto equipmentCreateDto)
    {
        var facility = await context.Facilities.FindAsync(equipmentCreateDto.FacilityId);
        if (facility == null)
        {
            throw new Exception("Facility with the given id not found");
        }
        
        var issue = await context.Issues.FindAsync(equipmentCreateDto.IssueId);
        if (issue == null)
        {
            throw new Exception("Facility with the given id not found");
        }

        var equipment = new Equipment()
        {
            Name = equipmentCreateDto.Name,
            Facility = facility,
            Issue = issue,
            Quantity = equipmentCreateDto.Quantity,
            CreateDateTime = DateTime.Now,
            CreateBy = "admin",
            IsInStock = equipmentCreateDto.Quantity >= 1
        };
        
        await context.Equipments.AddAsync(equipment);
        await context.SaveChangesAsync();
        
        return equipment.Id;
    }

    public async Task UpdateAsync(int equipmentId, EquipmentCreateDto equipmentUpdateDto)
    {
        var @equipment = await context.Equipments.FindAsync(equipmentId);
        if (@equipment == null)
        {
            throw new Exception("Equipment with the given id not found");
        }

        var facility = await context.Facilities.FindAsync(equipmentUpdateDto.FacilityId);
        if (facility == null)
        {
            throw new Exception("Facility with the given id not found");
        }
        
        var issue = await context.Issues.FindAsync(equipmentUpdateDto.IssueId);
        if (issue == null)
        {
            throw new Exception("Facility with the given id not found");
        }
        

        @equipment.Name = equipmentUpdateDto.Name;
        @equipment.Facility = facility;
        @equipment.Issue = issue;
        @equipment.Quantity = equipmentUpdateDto.Quantity;
        @equipment.IsInStock = equipmentUpdateDto.Quantity >= 1;
        @equipment.Name = equipmentUpdateDto.Name;
        
        context.Equipments.Update(@equipment);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int equipmentId)
    {
        var @equipment = await context.Equipments.FindAsync(equipmentId);
        if (@equipment == null)
        {
            throw new Exception("Equipment with the given id not found");
        }

        await context.Equipments.Where(e => e.Id == equipmentId).ExecuteDeleteAsync();
    }
}