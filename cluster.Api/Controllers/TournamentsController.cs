using cluster.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cluster.Api.Controllers
{
    [ApiController]
    [Route("/api/tournaments")]
    public class TournamentsController : ControllerBase
    {
        private readonly DataContext dataContext;

        public TournamentsController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await dataContext.Tournaments.Include(t => t.Matches).ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var tournament = await dataContext.Tournaments
                .Include(t => t.Matches)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (tournament == null)
            {
                return NotFound();
            }
            return Ok(tournament);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Tournament tournament)
        {
            dataContext.Tournaments.Add(tournament);
            await dataContext.SaveChangesAsync();
            return Ok(tournament);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Tournament tournament)
        {
            dataContext.Tournaments.Update(tournament);
            await dataContext.SaveChangesAsync();
            return Ok(tournament);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var affectedRows = await dataContext.Tournaments.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (affectedRows == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}