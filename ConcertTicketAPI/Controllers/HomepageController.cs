using ConcertTicketAPI.Data;
using ConcertTicketAPI.Models.Performer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConcertTicketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomepageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HomepageController(ApplicationDbContext context) => _context = context;

        // GET: api/Homepage
        [HttpGet]
        public async Task<ActionResult<List<PerformerCategory>>> GetHomepage()
        {
            var performerCats = await _context
                .PerformerCategories
                .Include(c => c.SubCategories
                    .Where(sc => sc.Performers.Count > 0))
                .ThenInclude(sc => sc.Performers
                    .OrderBy(p => p.Name)
                    .Take(10))
                .Where(c => c.InHeader)
                .Where(c => c.SubCategories.Count > 0)
                .ToListAsync();

            return performerCats;
        }
    }
}