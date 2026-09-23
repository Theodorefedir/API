using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoGames.DAL.Entities;
using VideoGames.DAL.Repositories;

namespace VideoGames.Controllers
{
    [ApiController]
    [Route("api/developer")]
    public class DeveloperController : ControllerBase
    {
        private readonly DeveloperRepository _developerRepository;

        public DeveloperController(DeveloperRepository developerRepository)
        {
            _developerRepository = developerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
        {
            int total = await _developerRepository.GetAll().CountAsync();
            int pages = (int)Math.Ceiling((double)total / pageSize);

            page = page < 1 || page > pages ? 1 : page;
            pageSize = pageSize < 1 ? 20 : pageSize;

            var developers = await _developerRepository
                .GetAll()
                .Include(d => d.Games)
                .OrderBy(d => d.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            var dtos = developers.Select(d => new DeveloperDto
            {
                Id = d.Id,
                Name = d.Name,
                Country = d.Country,
                Year = d.Year,
                Description = d.Description,
                Image = d.Image,
                Games = d.Games.Select(g => g.Name).ToList()
            });

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, CancellationToken ct = default)
        {
            var developer = await _developerRepository.GetByIdAsync(id, ct);

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
        public async Task<IActionResult> CreateAsync([FromBody] Developer developer, CancellationToken ct = default)
        {
            await _developerRepository.CreateAsync(developer, ct);

            return Ok("Developer added");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] Developer developer, CancellationToken ct = default)
        {
            await _developerRepository.UpdateAsync(developer, ct);

            return Ok("Developer updated");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody] Developer developer, CancellationToken ct = default)
        {
            await _developerRepository.DeleteAsync(developer, ct);

            return Ok("Developer deleted");
        }
    }
}
