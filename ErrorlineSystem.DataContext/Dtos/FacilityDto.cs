namespace ErrorlineSystem.DataContext.Dtos;

public class FacilityDto
{
    public int Id { get; set; }
    public String Name { get; set; }
    public List<EquipmentDto> Equipments { get; set; }
    public List<IssueDto> Issues { get; set; }
}