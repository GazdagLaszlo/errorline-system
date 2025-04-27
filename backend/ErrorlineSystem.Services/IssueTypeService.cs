using ErrorlineSystem.DataContext.Context;
using ErrorlineSystem.DataContext.Dtos;
using ErrorlineSystem.DataContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace ErrorlineSystem.Services;

public interface IIssueTypeService
{
    /// <summary>
    /// Lekéri az összes IssueTypeot.
    /// </summary>
    Task<IEnumerable<IssueTypeDto>> GetAllAsync();

    /// <summary>
    /// Lekér egy IssueTypeot az azonosító alapján.
    /// </summary>
    Task<IssueTypeDto> GetByIdAsync(int id);

    
    /// <summary>
    /// Létrehoz egy új IssueTypeot.
    /// </summary>
    Task<IssueTypeDto> CreateAsync(IssueTypeCreateDto issueTypeCreateDto);
    
    /// <summary>
    /// Frissít egy meglévő IssueTypeot.
    /// </summary>
    Task UpdateAsync(int id, IssueTypeCreateDto issueTypeUpdateDto);
    
    /// <summary>
    /// Töröl egy IssueTypeot az azonosító alapján.
    /// </summary>
    Task DeleteAsync(int id);
}

public class IssueTypeService(AppDbContext context) : IIssueTypeService
{
    public async Task<IEnumerable<IssueTypeDto>> GetAllAsync()
    {
        return await context.IssueTypes
            .Select(issueType => new IssueTypeDto()
            {
                Id = issueType.Id,
                Name = issueType.Name,
            }).ToListAsync();
    }

    public async Task<IssueTypeDto> GetByIdAsync(int id)
    {
        var @issueType = await context.IssueTypes.FindAsync(id);

        if (@issueType == null)
        {
            throw new Exception("IssueType with the given id not found");
        }
        
        return new IssueTypeDto()
        {
            Id = @issueType.Id,
            Name = @issueType.Name,
        };
    }

    public async Task<IssueTypeDto> CreateAsync(IssueTypeCreateDto issueTypeCreateDto)
    {
        var issueType = new IssueType()
        {
            Name = issueTypeCreateDto.Name,
        };
        
        await context.IssueTypes.AddAsync(issueType);
        await context.SaveChangesAsync();
        
        return new IssueTypeDto()
        {
            Id = @issueType.Id,
            Name = @issueType.Name,
        };
    }

    public async Task UpdateAsync(int issueTypeId, IssueTypeCreateDto issueTypeUpdateDto)
    {
        var @issueType = await context.IssueTypes.FindAsync(issueTypeId);
        if (@issueType == null)
        {
            throw new Exception("IssueType with the given id not found");
        }
        

        @issueType.Name = issueTypeUpdateDto.Name;
        
        context.IssueTypes.Update(@issueType);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int issueTypeId)
    {
        var @issueType = await context.IssueTypes.FindAsync(issueTypeId);
        if (@issueType == null)
        {
            throw new Exception("IssueType with the given id not found");
        }

        await context.IssueTypes.Where(e => e.Id == issueTypeId).ExecuteDeleteAsync();
    }
}