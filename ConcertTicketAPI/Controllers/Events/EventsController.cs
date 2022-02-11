#nullable disable
using ConcertTicketAPI.Data;
using ConcertTicketAPI.Dto;
using ConcertTicketAPI.Models.Event;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConcertTicketAPI.Controllers.Events
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context) => _context = context;

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<BaseListingDto<Event>>>
            GetEvents([FromQuery] PaginationDto pagination)
        {
            var events = await _context.Events
                .OrderByDescending(e => e.Date)
                .Skip(pagination.PerPage * (pagination.PageId - 1))
                .Take(pagination.PerPage)
                .ToListAsync();

            if (events.Count <= 0) return NotFound();

            pagination.Total = await _context.Events.CountAsync();
            pagination.LastPage = (pagination.Total / pagination.PerPage) + 1;

            return new BaseListingDto<Event>
            {
                Items = events,
                Pagination = pagination
            };
        }

        // GET: api/Events/slug-name
        [HttpGet("{slug}")]
        public async Task<ActionResult<Event>> GetEvent([FromRoute] string slug)
        {
            var @event = await _context
                .Events
                .Include(e => e.Venue)
                .Include(e => e.SubCategory)
                .ThenInclude(sc => sc.Category)
                .Where(e => e.Slug == slug)
                .FirstOrDefaultAsync();

            if (@event == null) return NotFound();

            return @event;
        }
    }
}