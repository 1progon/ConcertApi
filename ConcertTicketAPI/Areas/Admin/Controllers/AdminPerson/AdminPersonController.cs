#nullable disable
using System.Security.Claims;
using ConcertTicketAPI.Data;
using ConcertTicketAPI.Dto;
using ConcertTicketAPI.Models.Enums;
using ConcertTicketAPI.Models.Person;
using ConcertTicketAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConcertTicketAPI.Areas.Admin.Controllers.AdminPerson;

[ApiController]
[Area("Admin")]
[Route("api/[area]/Person")]
public class AdminPersonController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly TokenService _tokenService;

    public AdminPersonController(ApplicationDbContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }


    // GET: api/Admin/Person
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
    {
        return await _context.Persons.ToListAsync();
    }

    // GET: api/Admin/Person/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Person>> GetPerson(int id)
    {
        var person = await _context.Persons.FindAsync(id);

        if (person == null) return NotFound();

        return person;
    }


    // Get: api/Admin/Person/guid
    [HttpGet("{guid:guid}")]
    public async Task<ActionResult<PersonShortDto>> GetPersonShortByGuid(Guid guid)
    {
        var person = await _context
            .Persons
            .SingleOrDefaultAsync(p => p.Guid == guid);

        if (person == null) return NotFound();

        var jwtToken = _tokenService.GenerateJwtToken(person, out var expires);


        return new PersonShortDto
        {
            Guid = person.Guid,
            AccessToken = jwtToken,
            RefreshToken = person.RefreshToken ?? string.Empty,
            Email = person.Email,
            FirstName = person.FirstName,
            LastName = person.LastName,
            Type = person.Type
        };
    }

    [Authorize]
    // PUT: api/Admin/Person/email
    [HttpPut("{guid:guid}")]
    public async Task<IActionResult> PutPerson([FromRoute] Guid guid, [FromBody] PersonShortDto personDto)
    {
        if (guid.ToString() != User.Claims
                .First(c => c.Type == ClaimTypes.NameIdentifier).Value)
        {
            return BadRequest();
        }
        
        if (guid != personDto.Guid) return BadRequest();

        var person = await _context
            .Persons
            .SingleOrDefaultAsync(p => p.Guid == personDto.Guid
            );

        if (person == null) return BadRequest();

        // if (personDto.Token != person.Token)
        // {
        //     return Unauthorized();
        // }

        person.Email = personDto.Email;
        person.FirstName = personDto.FirstName;
        person.LastName = personDto.LastName;

        _context.Update(person);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            return Problem(statusCode: 500, title: "DB Saving error");
        }

        return NoContent();
    }


    // POST: api/Admin/Person
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Person>> PostPerson(Person person)
    {
        _context.Persons.Add(person);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetPerson", new {id = person.Id}, person);
    }

    // DELETE: api/Admin/Person/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(int id)
    {
        var person = await _context.Persons.FindAsync(id);
        if (person == null) return NotFound();

        _context.Persons.Remove(person);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PersonExists(int id)
    {
        return _context.Persons.Any(e => e.Id == id);
    }
}