#nullable disable
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using ConcertTicketAPI.Areas.Admin.AdminModels;
using ConcertTicketAPI.Areas.Admin.AdminViewModels;
using ConcertTicketAPI.Data;
using ConcertTicketAPI.Models.Enums;
using ConcertTicketAPI.Models.Location;
using ConcertTicketAPI.Models.Person;
using ConcertTicketAPI.Models.Phone;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.OpenApi.Extensions;
using Newtonsoft.Json.Schema;
using NuGet.Protocol;

namespace ConcertTicketAPI.Areas.Admin.Controllers.AdminPerson;

[Area("Admin")]
[Route("api/[area]/Person")]
[ApiController]
public class AdminPersonController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AdminPersonController(ApplicationDbContext context) => _context = context;


    // POST: api/Admin/Person/Login
    [HttpPost("Login")]
    public async Task<ActionResult<Person>> Login([FromBody] LoginModel model)
    {
        if (!ModelState.IsValid) return BadRequest(model);

        var person = await _context.Persons.SingleOrDefaultAsync(p => p.Email == model.Email);

        if (person == null) return NotFound();

        var ps = new PasswordHasher<Person>();

        var isPassword = ps.VerifyHashedPassword(person, person.Password, model.Password);

        if (isPassword == PasswordVerificationResult.Success)
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

            person.Token = token;
            person.TokenExpire = DateTime.UtcNow + TimeSpan.FromMinutes(10);

            _context.Persons.Update(person);
            await _context.SaveChangesAsync();

            return person;
        }

        return Unauthorized();
    }


    // GET: api/Admin/Person/Register
    [HttpGet("Register")]
    public async Task<ActionResult<RegisterViewModel>> Register(
        [FromQuery] int? countryId = null, [FromQuery] int? stateId = null)
    {
        IQueryable<LocationCountry> query = _context.LocationCountries;

        if (countryId != null & stateId != null)
        {
            query = query
                .Include(c => c.States
                    .Where(s => s.Id == stateId && s.Cities.Count > 0))
                .ThenInclude(s => s.Cities)
                .Where(c => c.Id == countryId);
        }
        else if (countryId != null)
        {
            query = query
                .Include(c => c.States
                    .Where(s => s.Cities.Count > 0))
                .Where(c => c.Id == countryId);
        }
        else
        {
            query = query
                .Where(c => c.States.Count > 0);
        }


        var genderTypes = new List<GenderTypeValues>();
        foreach (var item in Enum.GetValues<GenderType>())
        {
            genderTypes.Add(new GenderTypeValues
            {
                Name = item.GetAttributeOfType<DescriptionAttribute>()?.Description ?? item.ToString(),
                Value = (int) item,
            });
        }

        return new RegisterViewModel
        {
            Countries = await query.ToListAsync(),
            GenderTypes = genderTypes,
            PhoneCodes = _context
                .PhoneCodesCountries
                .Include(c => c.Country)
                .Where(c => c.Country.States.Count > 0)
                .Select(c => new
                {
                    c.Code, c.CountryId, c.Id
                }),
        };
    }

    // POST: api/Admin/Person/Register
    [HttpPost("Register")]
    public async Task<ActionResult<Person>> Register([FromBody] RegisterModel model)
    {
        var checkPersonInDb = await _context
            .Persons
            .SingleOrDefaultAsync(p => p.Email == model.Email);

        if (checkPersonInDb != null)
            return Conflict(new {Error = "User already exists, go to login page"});

        if (model.Password != model.PasswordConfirm)
            return Unauthorized(new {Error = "Passwords not equal"});

        var ps = new PasswordHasher<Person>();

        // new person model from register view model
        var person = new Person
        {
            Email = model.Email,

            CountryId = model.CountryId,
            StateId = model.StateId,
            CityId = model.CityId,

            Phone = string.IsNullOrWhiteSpace(model.Phone)
                ? null
                : $"{model.PhoneCode ?? "code"};;{model.Phone}",

            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            FirstName = model.FirstName,
            LastName = model.LastName,
            MiddleName = model.MiddleName,

            // Default User type
            Type = PersonType.User,
            Gender = model.Gender,
            BirthDate = model.BirthDate,
        };

        // hash password
        person.Password = ps.HashPassword(person, model.Password);

        // generate token
        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

        person.Token = token;
        person.TokenExpire = DateTime.UtcNow + TimeSpan.FromMinutes(10);


        try
        {
            throw new Exception("popa");
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
            return person;
        }
        catch (Exception e)
        {
            return Problem(
                statusCode: 500,
                title: "Save to DB problem",
                detail: e.InnerException?.Message
            );
        }
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

    // PUT: api/Admin/Person/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPerson(int id, Person person)
    {
        if (id != person.Id) return BadRequest();

        _context.Entry(person).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PersonExists(id))
                return NotFound();
            throw;
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