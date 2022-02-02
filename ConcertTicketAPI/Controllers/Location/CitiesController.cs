#nullable disable
using ConcertTicketAPI.Data;
using ConcertTicketAPI.Models.Location;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConcertTicketAPI.Controllers.Location
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CitiesController(ApplicationDbContext context) => _context = context;

        // GET: api/Cities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationCity>>> GetCities()
        {
            return await _context.LocationCities.ToListAsync();
        }

        //GET: api/Cities/PopularCities
        [HttpGet("EventsPopularCities")]
        public async Task<ActionResult<IEnumerable<LocationCity>>> GetPopularCitiesWithEvents()
        {
            return await _context
                .LocationCities
                .Include(c => c.State)
                .ThenInclude(rs => rs.Country)
                .Include(c => c.Events.Where(e => e.Performers.Count > 0))
                .ThenInclude(e => e.Performers)
                .Where(c => c.Events.Count > 0)
                .OrderBy(c => c.Name)
                .Take(15)
                .ToListAsync();
        }

        //api/Cities/UsersCitiesFindByQuery
        [HttpGet("UsersCitiesFindByQuery")]
        public async Task<ActionResult<IEnumerable<LocationCity>>> GetUsersCitiesFindByQuery(
            [FromQuery] string query = null)
        {
            return await _context
                .LocationCities
                .Include(c => c.State)
                .ThenInclude(rs => rs.Country)
                .Where(c =>
                    c.Slug.Contains(query)
                    || c.Name.Contains(query)
                    || c.ShortName.Contains(query))
                .OrderBy(c => c.Name)
                .Take(10)
                .ToListAsync();
        }

        // api/Cities/GetUserPopularCities
        [HttpGet("GetUserPopularCities")]
        public async Task<ActionResult<IList<LocationCity>>> GetUserPopularCities()
        {
            return await _context
                .LocationCities
                .Include(c => c.State)
                .ThenInclude(rs => rs.Country)
                .Where(c => c.Popular)
                .OrderBy(c => c.Name)
                .Take(5)
                .ToListAsync();
        }

        // api/Cities/GetCityById/id
        [HttpGet("GetCityById/{id:int}")]
        public async Task<ActionResult<LocationCity>> GetCityById(int id)
        {
            return await _context
                .LocationCities
                .Include(c => c.State)
                .ThenInclude(rs => rs.Country)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        // GET: api/Cities/slug-name
        [HttpGet("{slug}")]
        public async Task<ActionResult<LocationCity>> GetCity([FromRoute] string slug)
        {
            var city = await _context.LocationCities.Where(c => c.Slug == slug).FirstOrDefaultAsync();

            if (city == null) return NotFound();

            return city;
        }
    }
}