using cluster.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace cluster.Api.Controllers
{
    [ApiController]
    [Route("/api/countries")]

    public class CountriesController:ControllerBase
    {
        private readonly DataContext dataContext;

        public CountriesController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        [HttpGet]

        public async Task<IActionResult> GetAsync()
        {
            return Ok(await dataContext.Countries.ToListAsync());
        }

        [HttpGet("id:int")]

        public async Task<IActionResult> GetAsync(int id)
        {
            var country = await
                dataContext.Countries.FirstOrDefaultAsync(x => x.Id == id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }
        [HttpPost]

        public async Task<IActionResult> PostAsync(Country country)
        {
            dataContext.Countries.Add(country);
            await dataContext.SaveChangesAsync();
            return Ok(country);
        }

        [HttpPut]

        public async Task<IActionResult> PutAsync(Country country)
        {
            dataContext.Countries.Update(country);
            await dataContext.SaveChangesAsync();
            return Ok(country);
        }

        [HttpDelete("id:int")]
        
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var afectedRows = await dataContext.Countries.Where(x => x.Id == id).ExecuteDeleteAsync();
            if(afectedRows==0)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}