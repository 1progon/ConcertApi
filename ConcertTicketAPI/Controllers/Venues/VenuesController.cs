#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConcertTicketAPI.Data;
using ConcertTicketAPI.Dto;
using ConcertTicketAPI.Models.Venue;

namespace ConcertTicketAPI.Controllers.Venues
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenuesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VenuesController(ApplicationDbContext context) => _context = context;

        // GET: api/Venues
        [HttpGet]
        public async Task<ActionResult<BaseListingDto<Venue>>>
            GetVenues([FromQuery] PaginationDto pagination)
        {
            var venues = await _context.Venues
                .OrderBy(v => v.Name)
                .Skip(pagination.PerPage * (pagination.PageId - 1))
                .Take(pagination.PerPage)
                .ToListAsync();

            if (venues.Count <= 0) return NotFound();

            pagination.Total = await _context.Venues.CountAsync();
            pagination.LastPage = pagination.Total / pagination.PerPage;


            return new BaseListingDto<Venue>()
            {
                Items = venues,
                Pagination = pagination
            };
        }

        // GET: api/Venues/slug-name
        [HttpGet("{slug}")]
        public async Task<ActionResult<Venue>> GetVenue([FromRoute] string slug)
        {
            var venue = await _context
                .Venues
                .Where(v => v.Slug == slug)
                .FirstOrDefaultAsync();

            if (venue == null) return NotFound();

            return venue;
        }
    }
}