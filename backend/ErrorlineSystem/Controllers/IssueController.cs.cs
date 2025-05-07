using System.Security.Claims;
using ErrorlineSystem.DataContext.Dtos;
using ErrorlineSystem.DataContext.Entities;
using ErrorlineSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErrorlineSystem.Controllers;

/// <summary>
/// A hibajegyek kezelésére szolgáló vezérlő
/// </summary>
[Route("api/[controller]/[action]")]
[ApiController]
[Authorize]
public class IssueController(IIssueService issueService) : ControllerBase
{
    private int UserId => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
    /// <summary>
    /// A hibajegyek listázása
    /// </summary>
    /// <returns> A rendszerben található hibajegyek listája </returns>
    [HttpGet]
    [ProducesResponseType<IEnumerable<IssueResponseDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllIssue()
    {
        return Ok(await issueService.ListAsync(UserId));
    }

    /// <summary>
    /// Hibajegy részleteinek lekérése ID alapján
    /// </summary>
    /// <param name="id">  Hibajegy ID-ja </param>
    /// <returns> A hibajegy részletei </returns>
    [HttpGet("{id}")]
    [ProducesResponseType<IssueResponseDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetIssue(int id)
    {
        return Ok(await issueService.GetIssueByIdAsync(id, UserId));
    }

    /// <summary>
    /// Hibajegy létrehozása
    /// </summary>
    /// <param name="dto"> A hibajegy adatai </param>
    /// <returns> A művelet eredményéről üzenet </returns>
    [HttpPost]
    [Authorize(Roles = "Resident")]
    [ProducesResponseType<IssueResponseDto>(StatusCodes.Status200OK)] 
    public async Task<IActionResult> CreateIssue([FromBody] IssueRequestDto dto)
    {
        IssueResponseDto response = await issueService.CreateIssueAsync(dto, UserId);
        return Ok(response);
    }

    /// <summary>
    /// Hibajegy módosítása
    /// </summary>
    /// <param name="id"> Hibajegy ID-ja </param>
    /// <param name="issueDto"> A módosult hibajegy adatai </param>
    /// <returns> A művelet eredményéről üzenet </returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> ModifyIssue(int id, [FromBody] IssueUpdateDto issueDto)
    {

        bool result = await issueService.ModifyIssueAsync(id, issueDto, UserId);
        if (!result)
        {
            return BadRequest("Nem sikerült a hibajegy módosítása");
        }
        return Ok("Sikeres végrehajtás");

    }

    /// <summary>
    /// Hibajegy státuszának módosítása
    /// </summary>
    /// <param name="id"> A hibajegy ID-ja </param>
    /// <param name="state"> Az új állapot amire állítani kell </param>
    /// <returns> A művelet eredményéről üzenet </returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> ModifyIssueState(int id, [FromBody] IssueState state)
    {
        bool result = await issueService.ModifyStateAsync(id, state, UserId);
        if (!result)
        {
            return BadRequest("Nem sikerült a státusz módosítása");
        }
        return Ok("Sikeres végrehajtás");
    }

    /// <summary>
    /// Komment hozzárendelése a hibajegyhez
    /// </summary>
    /// <param name="id"> A hibajegy ID-ja </param>
    /// <param name="dto"> A komment adatai </param>
    /// <returns> A művelet eredményéről üzenet </returns>
    [HttpPost("{id}")]
    public async Task<IActionResult> AddComment(int id, [FromBody] CommentDto dto)
    {
        bool result = await issueService.AddComment(id, dto);
        if (!result)
        {
            return BadRequest("Nem sikerült a komment hozzáadása");
        }
        return Ok("Sikeres végrehajtás");
    }

    /// <summary>
    /// Komment törlése
    /// </summary>
    /// <param name="id"> Komment ID-ja </param>
    /// <returns> A művelet eredményéről üzenet </returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        bool result = await issueService.DeleteComment(id);
        if (!result)
        {
            return BadRequest("Nem sikerült a komment törlése");
        }
        return Ok("Sikeres végrehajtás");
    }
}