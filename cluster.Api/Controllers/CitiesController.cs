using cluster.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace cluster.Api.Controllers
{
    [ApiController]
    [Route("/api/cities")]

    public class CitiesController : ControllerBase
    {
        private readonly DataContext dataContext;

        public CitiesController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        [HttpGet]

        public async Task<IActionResult> GetAsync()
        {
            return Ok(await dataContext.Cities.ToListAsync());
        }

        [HttpGet("id:int")]

        public async Task<IActionResult> GetAsync(int id)
        {
            var city = await
                dataContext.Cities.FirstOrDefaultAsync(x => x.Id == id);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }
        [HttpPost]

        public async Task<IActionResult> PostAsync(City city)
        {
            dataContext.Cities.Add(city);
            await dataContext.SaveChangesAsync();
            return Ok(city);
        }

        [HttpPut]

        public async Task<IActionResult> PutAsync(City city)
        {
            dataContext.Cities.Update(city);
            await dataContext.SaveChangesAsync();
            return Ok(city);
        }

        [HttpDelete("id:int")]

        public async Task<IActionResult> DeleteAsync(int id)
        {
            var afectedRows = await dataContext.Countries.Where(x => x.Id == id).ExecuteDeleteAsync();
            if (afectedRows == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}