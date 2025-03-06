namespace ErrorlineSystem.DataContext.Entities;

public class Equipment
{
    public int Id { get; set; }
    public String Name { get; set; }
    public Boolean IsInStock { get; set; }
    public Issue Issue { get; set; }
    public Facility Facility { get; set; }
    public List<EquipmentOrder> EquipmentOrders { get; set; }
    
    public DateTime CreateDateTime { get; set; }
    public String CreateBy { get; set; }
}