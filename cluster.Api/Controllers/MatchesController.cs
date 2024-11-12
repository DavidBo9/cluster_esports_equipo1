using System.Text.RegularExpressions;
using cluster.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cluster.Api.Controllers
{
    [ApiController]
    [Route("/api/matches")]
    public class MatchesController : ControllerBase
    {
        private readonly DataContext dataContext;

        public MatchesController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await dataContext.Matches
                .Include(m => m.Tournament)
                .Include(m => m.Team1)
                .Include(m => m.Team2)
                .ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var match = await dataContext.Matches
                .Include(m => m.Tournament)
                .Include(m => m.Team1)
                .Include(m => m.Team2)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (match == null)
            {
                return NotFound();
            }
            return Ok(match);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Shared.Entities.Match match)
        {
            dataContext.Matches.Add(match);
            await dataContext.SaveChangesAsync();
            return Ok(match);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Shared.Entities.Match match)
        {
            dataContext.Matches.Update(match);
            await dataContext.SaveChangesAsync();
            return Ok(match);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var affectedRows = await dataContext.Matches.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (affectedRows == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}