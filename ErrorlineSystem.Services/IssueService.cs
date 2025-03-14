using ErrorlineSystem.DataContext.Context;
using ErrorlineSystem.DataContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorlineSystem.Services
{
    public interface IIssueService
    {
        List<Issue> List();
    }

    public class IssueService(AppDbContext context) : IIssueService
    {
        public List<Issue> List()
        {
            return context.Issues.ToList();
        }
    }
}
