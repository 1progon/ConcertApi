#nullable disable
using ConcertTicketAPI.Data;
using ConcertTicketAPI.Models.Performer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConcertTicketAPI.Controllers.Performers
{
    [Route("api/[controller]")] // api/PerformerCategories
    [ApiController]
    public class PerformerCategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PerformerCategoriesController(ApplicationDbContext context) => _context = context;

        // GET: api/PerformerCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PerformerCategory>>> GetCategories()
        {
            return await _context.PerformerCategories.ToListAsync();
        }

        // GET: api/PerformerCategories/slug-name
        [HttpGet("{slug}")]
        public async Task<ActionResult<PerformerCategory>> GetCategoryBySlug(string slug)
        {
            var category = await _context
                .PerformerCategories
                .Include(c => c.SubCategories
                    .Where(sc => sc.Performers.Count > 0))
                .ThenInclude(sc => sc.Performers
                    .OrderBy(p => p.Name)
                    .Take(10))
                .Where(c => c.Slug == slug)
                .FirstOrDefaultAsync();

            return category;
        }
    }
}