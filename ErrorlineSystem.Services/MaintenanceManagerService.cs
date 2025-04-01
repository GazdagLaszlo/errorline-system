using AutoMapper;
using ErrorlineSystem.DataContext.Context;
using ErrorlineSystem.DataContext.Entities;
using ErrorlineSystem.DataContext.Dtos;
using Microsoft.EntityFrameworkCore;

namespace ErrorlineSystem.Services;

public interface IMaintenanceManagerService
{
    Task<IList<IssueDto>> GetOpenIssuesAsync();
    Task<bool> UpdateIssueStateAsync(int issueId);
    Task<Dictionary<int, int>> AssignIssueAsync();
}
public class MaintenanceManagerService(AppDbContext context, IMapper mapper) : IMaintenanceManagerService
{
    public async Task<IList<IssueDto>> GetOpenIssuesAsync()
    {
        var issues = await context.Issues
            .Where(x => x.State == IssueState.Open)
            .ToListAsync();
        return mapper.Map<IList<IssueDto>>(issues);
    }
    public Task<Dictionary<int, int>> AssignIssueAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateIssueStateAsync(int issueId)
    {
        throw new NotImplementedException();
    }
}
