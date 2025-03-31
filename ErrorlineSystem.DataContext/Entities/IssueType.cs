using System.ComponentModel.DataAnnotations;

namespace ErrorlineSystem.DataContext.Entities;

public class IssueType
{
    [Key]
    public int Id { get; set; }
    public String Name { get; set; }
    public List<Issue> Issues { get; set; }
}