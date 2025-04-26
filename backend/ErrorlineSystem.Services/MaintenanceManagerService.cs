using AutoMapper;
using ErrorlineSystem.DataContext.Context;
using ErrorlineSystem.DataContext.Entities;
using ErrorlineSystem.DataContext.Dtos;
using Microsoft.EntityFrameworkCore;

namespace ErrorlineSystem.Services;

public interface IMaintenanceManagerService
{
    Task<IList<IssueDto>> GetOpenIssuesAsync();
    Task<IssueDto> UpdateIssueStateAsync(int issueId, IssueState state);
    Task<IssueAssignDto> AssignIssueAsync(int issueId, int assignedUserId);
}
public class MaintenanceManagerService(AppDbContext context, IMapper mapper) : IMaintenanceManagerService
{
    public async Task<IList<IssueDto>> GetOpenIssuesAsync()
    {
        var issues = await context.Issues
            .Where(x => x.State == IssueState.Open)
            .Include(x => x.AssignedUser)
            .Include(x => x.ModifiedBy)
            .ToListAsync();
        return mapper.Map<IList<IssueDto>>(issues);
    }
    public async Task<IssueAssignDto> AssignIssueAsync(int issueId, int assignedUserId)
    {
        var issue = await context.Issues.FindAsync(issueId);
        var user = await context.Users.FindAsync(assignedUserId);
        if(issue == null)
        {
            throw new KeyNotFoundException($"Issue not found with id: {issueId}");
        }
        if (user == null)
        {
            throw new KeyNotFoundException($"User not found with id: {assignedUserId}");
        }

        if (issue.State == IssueState.Open) { 
            issue.State = IssueState.InProgress;
            issue.AssignedUser = user;
            await context.SaveChangesAsync();
        }

        var response = new IssueAssignDto
        {
            IssueId = issueId,
            AssignedUser = user.Name,
        };
        return response;
    }

    public async Task<IssueDto> UpdateIssueStateAsync(int issueId, IssueState state)
    {
        var issue = await context.Issues.FindAsync(issueId);
        if (issue == null)
        {
            throw new KeyNotFoundException($"Issue not found with id: {issueId}");
        }

        issue.State = state;
        await context.SaveChangesAsync();
        return mapper.Map<IssueDto>(issue);
    }
}
