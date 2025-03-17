using System.ComponentModel.DataAnnotations;

namespace ErrorlineSystem.DataContext.Entities;

public class Facility
{
    [Key]
    public int Id { get; set; }
    public String Name { get; set; }
    public List<Equipment> Equipments { get; set; }
    public List<Issue> Issues { get; set; }
}