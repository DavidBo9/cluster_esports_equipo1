// Controllers/PlayersController.cs
using cluster.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cluster.Api.Controllers
{
    [ApiController]
    [Route("/api/players")]
    public class PlayersController : ControllerBase
    {
        private readonly DataContext dataContext;

        public PlayersController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await dataContext.Players.Include(p => p.Team).ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var player = await dataContext.Players
                .Include(p => p.Team)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(player);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Player player)
        {
            dataContext.Players.Add(player);
            await dataContext.SaveChangesAsync();
            return Ok(player);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Player player)
        {
            dataContext.Players.Update(player);
            await dataContext.SaveChangesAsync();
            return Ok(player);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var affectedRows = await dataContext.Players.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (affectedRows == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}