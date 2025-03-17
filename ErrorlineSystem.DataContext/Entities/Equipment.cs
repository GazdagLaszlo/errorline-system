using System.ComponentModel.DataAnnotations;

namespace ErrorlineSystem.DataContext.Entities;

public class Equipment
{
    [Key]
    public int Id { get; set; }
    [Required]
    public String Name { get; set; }
    public Boolean IsInStock { get; set; }
    [Required]
    public Issue Issue { get; set; }
    public Facility Facility { get; set; }
    public List<EquipmentOrder> EquipmentOrders { get; set; }
    public DateTime CreateDateTime { get; set; }
    [Required]
    public String CreateBy { get; set; }
}