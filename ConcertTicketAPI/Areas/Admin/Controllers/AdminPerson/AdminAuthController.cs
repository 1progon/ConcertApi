using System.ComponentModel;
using System.Security.Cryptography;
using ConcertTicketAPI.Areas.Admin.AdminDto;
using ConcertTicketAPI.Data;
using ConcertTicketAPI.Dto;
using ConcertTicketAPI.Models.Enums;
using ConcertTicketAPI.Models.Location;
using ConcertTicketAPI.Models.Person;
using ConcertTicketAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

namespace ConcertTicketAPI.Areas.Admin.Controllers.AdminPerson;

[ApiController]
[Area("Admin")]
[Route("api/[area]/AuthPerson")]
public class AdminAuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AdminAuthController(ApplicationDbContext context) => _context = context;

    // POST: api/Admin/Person/Login
    [HttpPost("Login")]
    public async Task<ActionResult<PersonShortDto>> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid) return BadRequest(loginDto);

        var person = await _context.Persons.SingleOrDefaultAsync(p => p.Email == loginDto.Email);

        if (person == null) return NotFound(new ErrorDto() {Error = "User not found"});

        var ps = new PasswordHasher<Person>();

        var isPassword = ps.VerifyHashedPassword(person, person.Password, loginDto.Password);

        if (isPassword != PasswordVerificationResult.Success)
            return Unauthorized(new ErrorDto {Error = "Have you forgotten your password?"});


        var token = TokenService.GenerateBase64UrlToken();

        person.Token = token;
        person.TokenExpire = DateTime.UtcNow + TimeSpan.FromMinutes(10);

        _context.Persons.Update(person);
        await _context.SaveChangesAsync();


        return new PersonShortDto
        {
            Guid = person.Guid,
            Email = person.Email,
            Token = person.Token,
            TokenExpire = person.TokenExpire,
            Type = person.Type,
            FirstName = person.FirstName,
            LastName = person.LastName,
        };
    }


    // GET: api/Admin/Person/Register
    [HttpGet("Register")]
    public async Task<ActionResult<RegisterGetDto>>
        Register([FromQuery] int? countryId = null, [FromQuery] int? stateId = null)
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


        // Add Gender Types to List
        var genderTypes = new List<GenderTypeValues>();
        foreach (var item in Enum.GetValues<GenderType>())
        {
            var attr = item.GetAttributeOfType<DescriptionAttribute>()?.Description;
            genderTypes.Add(new GenderTypeValues
            {
                Name = attr ?? item.ToString(),
                Value = (int) item,
            });
        }

        return new RegisterGetDto
        {
            Countries = await query.ToListAsync(),
            GenderTypes = genderTypes,
            PhoneCodes = _context
                .PhoneCodesCountries
                .Include(c => c.Country)
                .Where(c => c.Country.States.Count > 0)
                .Select(c => new CountryPhoneCodeDto
                {
                    Code = c.Code,
                    CountryId = c.CountryId,
                }),
        };
    }


    // POST: api/Admin/Person/Register
    [HttpPost("Register")]
    public async Task<ActionResult<PersonShortDto>>
        Register([FromBody] RegisterPostDto registerPostDto)
    {
        var checkPersonInDb = await _context
            .Persons
            .AnyAsync(p => p.Email == registerPostDto.Email);

        if (checkPersonInDb)
            return Conflict(new ErrorDto {Error = "User already exists, go to login page"});

        if (registerPostDto.Password != registerPostDto.PasswordConfirm)
            return Unauthorized(new ErrorDto {Error = "Passwords not equal"});

        var ps = new PasswordHasher<Person>();

        // new person model from register view model
        var person = new Person
        {
            Email = registerPostDto.Email,
            Guid = Guid.NewGuid(),

            CountryId = registerPostDto.CountryId,
            StateId = registerPostDto.StateId,
            CityId = registerPostDto.CityId,

            Phone = string.IsNullOrWhiteSpace(registerPostDto.Phone)
                ? null
                : $"{registerPostDto.PhoneCode ?? "code"};;{registerPostDto.Phone}",

            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            FirstName = registerPostDto.FirstName,
            LastName = registerPostDto.LastName,
            MiddleName = registerPostDto.MiddleName,

            // Default User type
            // Type = PersonType.User,
            Gender = registerPostDto.Gender,
            BirthDate = registerPostDto.BirthDate,
        };

        // hash password
        person.Password = ps.HashPassword(person, registerPostDto.Password);

        // generate token
        var token = TokenService.GenerateBase64UrlToken();

        person.Token = token;
        person.TokenExpire = DateTime.UtcNow + TimeSpan.FromMinutes(10);


        try
        {
            // throw new Exception("error");
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return new PersonShortDto
            {
                Guid = person.Guid,
                Email = person.Email,
                Token = person.Token,
                TokenExpire = person.TokenExpire,
                Type = person.Type,
                FirstName = person.FirstName,
                LastName = person.LastName,
            };
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
}