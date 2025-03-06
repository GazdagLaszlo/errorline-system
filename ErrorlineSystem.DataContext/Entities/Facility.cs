namespace ErrorlineSystem.DataContext.Entities;

public class Facility
{
    public int Id { get; set; }
    public String Name { get; set; }
    public List<Equipment> Equipments { get; set; }
    public List<Issue> Issues { get; set; }
}