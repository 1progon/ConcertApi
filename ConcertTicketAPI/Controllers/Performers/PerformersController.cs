#nullable disable
using System.IdentityModel.Tokens.Jwt;
using ConcertTicketAPI.Data;
using ConcertTicketAPI.Dto;
using ConcertTicketAPI.Models.Enums;
using ConcertTicketAPI.Models.Performer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ConcertTicketAPI.Controllers.Performers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PerformersController(ApplicationDbContext context) => _context = context;

        // GET: api/Performers
        [HttpGet]
        public async Task<ActionResult<BaseListingDto<Performer>>>
            GetPerformers([FromQuery] PaginationDto pagination)
        {
            var performers = await _context.Performers
                .Include(p => p.Parent)
                .OrderBy(p => p.Name)
                .Skip(pagination.PerPage * (pagination.PageId - 1))
                .Take(pagination.PerPage)
                .ToListAsync();

            if (performers.Count <= 0) return NotFound();

            pagination.Total = await _context.Performers.CountAsync();
            pagination.LastPage = (pagination.Total / pagination.PerPage) + 1;

            return new BaseListingDto<Performer>()
            {
                Items = performers,
                Pagination = pagination,
            };
        }

        // GET: api/Performers/slug-name
        [HttpGet("{slug}")]
        public async Task<ActionResult<Performer>> GetPerformer(string slug)
        {
            var performer = await _context
                .Performers
                .Include(p => p.SubCategory)
                .ThenInclude(sc => sc.Category)
                .Include(p => p.Events
                    .Where(e => e.Tickets.Count > 0))
                .Include(p => p.Parent)
                .Where(p => p.Slug == slug)
                .FirstOrDefaultAsync();

            if (performer?.Type == PerformerType.Grouping)
            {
                // Get child performers, only if grouping
                performer.Members = await _context
                    .Performers
                    .Where(cp => cp.ParentId == performer.Id)
                    .ToListAsync();
            }


            return performer;
        }
    }
}