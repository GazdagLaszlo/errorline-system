namespace ErrorlineSystem.DataContext.Dtos;

public class EquipmentCreateDto
{
    public String Name { get; set; }
    public int IssueId { get; set; }
    public int FacilityId { get; set; }
    public int Quantity { get; set; }
}

public class EquipmentDto 
{
    public int Id { get; set; }
    public String Name { get; set; }
    public Boolean IsInStock { get; set; }
    public int Quantity { get; set; }
    public int IssueId { get; set; }
    public FacilityDto Facility { get; set; }
    public DateTime CreateDateTime { get; set; }
    public String CreateBy { get; set; }
}

public class EquipmentSearchListItemDto
{
    public int Id { get; set; }
    public String Name { get; set; }
    public Boolean IsInStock { get; set; }
    public int Quantity { get; set; }
    public int IssueId { get; set; }
    public int FacilityId { get; set; }
    public DateTime CreateDateTime { get; set; }
    public String CreateBy { get; set; }
}