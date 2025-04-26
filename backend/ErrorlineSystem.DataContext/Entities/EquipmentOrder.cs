using System.ComponentModel.DataAnnotations;

namespace ErrorlineSystem.DataContext.Entities;

public class EquipmentOrder
{
    [Key]
    public int Id { get; set; }
    public Issue Issue { get; set; }
    public Equipment Equipment { get; set; }
    public int Quantity { get; set; }
    public EquipmentOrderState State { get; set; } = EquipmentOrderState.Open;
    
    public enum EquipmentOrderState
    {
        Open, 
        OnGoing, 
        Completed
    }
}