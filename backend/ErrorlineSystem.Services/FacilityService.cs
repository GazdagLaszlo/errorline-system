using ErrorlineSystem.DataContext.Context;
using ErrorlineSystem.DataContext.Entities;

namespace ErrorlineSystem.Services;

public interface IFacilityService
{
    List<Facility> List();
}

public class FacilityService(AppDbContext context) : IFacilityService
{
    public List<Facility> List()
    {
        return context.Facilities.ToList();
    }
}