using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoGames.BLL.Dtos;
using VideoGames.BLL.Services;
using VideoGames.DAL.Entities;
using VideoGames.DAL.Repositories;

namespace VideoGames.Controllers
{
    [ApiController]
    [Route("api/developer")]
    public class DeveloperController : ControllerBase
    {
        private readonly DeveloperRepository _developerRepository;
        private readonly DeveloperService _developerService;

        public DeveloperController(DeveloperRepository developerRepository, DeveloperService developerService)
        {
            _developerRepository = developerRepository;
            _developerService = developerService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync(CancellationToken ct = default)
        {
            var items = await _developerService.GetAllAsync(ct);
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, CancellationToken ct = default)
        {
            var developer = await _developerService.GetByIdAsync(id, ct);

            if (developer != null)
            {
                return Ok(developer);
            }
            else
            {
                return NotFound($"couldn't find a developer with  '{id}'");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateDeveloperDto dto, CancellationToken ct = default)
        {
            await _developerService.CreateAsync(dto, ct);

            return Ok("Developer added");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateDeveloperDto dto, CancellationToken ct = default)
        {
            await _developerService.UpdateAsync(dto, ct);

            return Ok("Developer updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, CancellationToken ct = default)
        {
            await _developerService.DeleteAsync(id, ct);

            return Ok("Developer deleted");
        }
    }
}
