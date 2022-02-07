#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConcertTicketAPI.Data;
using ConcertTicketAPI.Models.Location;

namespace ConcertTicketAPI.Controllers.Location
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/States
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationState>>> GetLocationStates()
        {
            return await _context.LocationStates.ToListAsync();
        }

        // GET: api/States/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LocationState>> GetLocationState(int id)
        {
            var locationState = await _context.LocationStates.FindAsync(id);

            if (locationState == null)
            {
                return NotFound();
            }

            return locationState;
        }

        // PUT: api/States/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocationState(int id, LocationState locationState)
        {
            if (id != locationState.Id)
            {
                return BadRequest();
            }

            _context.Entry(locationState).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationStateExists(id))
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

        // POST: api/States
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LocationState>> PostLocationState(LocationState locationState)
        {
            _context.LocationStates.Add(locationState);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocationState", new { id = locationState.Id }, locationState);
        }

        // DELETE: api/States/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocationState(int id)
        {
            var locationState = await _context.LocationStates.FindAsync(id);
            if (locationState == null)
            {
                return NotFound();
            }

            _context.LocationStates.Remove(locationState);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LocationStateExists(int id)
        {
            return _context.LocationStates.Any(e => e.Id == id);
        }
    }
}
