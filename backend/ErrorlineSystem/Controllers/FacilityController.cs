using ErrorlineSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErrorlineSystem.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
//[Authorize]
public class FacilityController(IFacilityService facilityService) : ControllerBase
{
    [HttpGet]
    public IActionResult List()
    {
        var result = facilityService.List();
        return Ok(result);
    }
}