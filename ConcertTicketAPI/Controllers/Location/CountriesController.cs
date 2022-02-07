#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConcertTicketAPI.Data;
using ConcertTicketAPI.Models.Location;

namespace ConcertTicketAPI.Controllers.Location
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CountriesController(ApplicationDbContext context) => _context = context;

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationCountry>>> GetLocationCountries()
        {
            return await _context
                .LocationCountries
                .ToListAsync();
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LocationCountry>> GetLocationCountry(int id)
        {
            var locationCountry = await _context.LocationCountries.FindAsync(id);

            if (locationCountry == null)
            {
                return NotFound();
            }

            return locationCountry;
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocationCountry(int id, LocationCountry locationCountry)
        {
            if (id != locationCountry.Id)
            {
                return BadRequest();
            }

            _context.Entry(locationCountry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationCountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LocationCountry>> PostLocationCountry(LocationCountry locationCountry)
        {
            _context.LocationCountries.Add(locationCountry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocationCountry", new { id = locationCountry.Id }, locationCountry);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocationCountry(int id)
        {
            var locationCountry = await _context.LocationCountries.FindAsync(id);
            if (locationCountry == null)
            {
                return NotFound();
            }

            _context.LocationCountries.Remove(locationCountry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LocationCountryExists(int id)
        {
            return _context.LocationCountries.Any(e => e.Id == id);
        }
    }
}
