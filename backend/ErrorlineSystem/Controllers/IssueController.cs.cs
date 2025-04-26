using ErrorlineSystem.DataContext.Dtos;
using ErrorlineSystem.DataContext.Entities;
using ErrorlineSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Mono.TextTemplating;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ErrorlineSystem.Controllers
{
    /// <summary>
    /// A hibajegyek kezelésére szolgáló vezérlő
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IIssueService _issueService;

        public IssueController(IIssueService issueService)
        {
            _issueService = issueService;
        }

        /// <summary>
        /// A hibajegyek listázása
        /// </summary>
        /// <returns> A rendszerben található hibajegyek listája </returns>
        [HttpGet]
        public async Task<IActionResult> GetAllIssue()
        {
            return Ok(await _issueService.ListAsync());
        }

        /// <summary>
        /// Hibajegy részleteinek lekérése ID alapján
        /// </summary>
        /// <param name="id">  Hibajegy ID-ja </param>
        /// <returns> A hibajegy részletei </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIssue(int id)
        {
            return Ok(await _issueService.GetIssueByIdAsync(id));
        }

        /// <summary>
        /// Hibajegy létrehozása
        /// </summary>
        /// <param name="dto"> A hibajegy adatai </param>
        /// <returns> A művelet eredményéről üzenet </returns>
        [HttpPost]
        public async Task<IActionResult> CreateIssue([FromBody] IssueRequestDto dto)
        {
            _issueService.CreateIssueAsync(dto);
            return Ok("Sikeres végrehajtás");
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

            bool result = await _issueService.ModifyIssueAsync(id, issueDto);
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
            bool result = await _issueService.ModifyStateAsync(id, state);
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
            bool result = await _issueService.AddComment(id, dto);
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
            bool result = await _issueService.DeleteComment(id);
            if (!result)
            {
                return BadRequest("Nem sikerült a komment törlése");
            }
            return Ok("Sikeres végrehajtás");
        }
    }
}
