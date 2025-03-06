namespace ErrorlineSystem.DataContext.Entities;

public class EquipmentOrder
{
    public int Id { get; set; }
    public Issue Issue { get; set; }
    public Equipment Equipment { get; set; }
    public int Quantity { get; set; }
    public EquipmentOrderState State { get; set; }
    
    public enum EquipmentOrderState
    {
        Open, 
        OnGoing, 
        Completed
    }
}