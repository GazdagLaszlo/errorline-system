using System.ComponentModel.DataAnnotations;
using ErrorlineSystem.DataContext.Entities;

namespace ErrorlineSystem.DataContext.Dtos;

public class EquipmentOrderDto
{
    public int Id { get; set; }
    public IssueDto Issue { get; set; }
    public EquipmentDto Equipment { get; set; }
    public int Quantity { get; set; }
    public string State { get; set; }
}

public class EquipmentOrderCreateDto
{
    [Required(ErrorMessage ="A probléma azonosítóját kötelező megadni!")]
    public int IssueId { get; set; }
    [Required(ErrorMessage ="Az eszköz azonosítóját kötelező megadni!")]
    public int EquipmentId { get; set; }
    [Required(ErrorMessage ="A mennyiséget kötelező megadni!")]
    public int Quantity { get; set; }
}