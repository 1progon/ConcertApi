#nullable disable
using ConcertTicketAPI.Data;
using ConcertTicketAPI.Models.Location;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConcertTicketAPI.Areas.Admin.Controllers.AdminLocations
{
    [Area("Admin")]
    [Route("api/[area]/LocationCountries")]
    [ApiController]
    // [Authorize(Roles = "Administrator")]
    public class AdminLocationCountriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdminLocationCountriesController(ApplicationDbContext context) => _context = context;

        // GET: api/AdminLocationCountries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationCountry>>> GetLocationCountries()
        {
            return await _context.LocationCountries.ToListAsync();
        }

        // GET: api/AdminLocationCountries/5
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

        // PUT: api/AdminLocationCountries/5
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

        // POST: api/AdminLocationCountries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LocationCountry>> PostLocationCountry(LocationCountry locationCountry)
        {
            _context.LocationCountries.Add(locationCountry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocationCountry", new {id = locationCountry.Id}, locationCountry);
        }

        // DELETE: api/AdminLocationCountries/5
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