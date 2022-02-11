using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Extensions;

namespace ConcertTicketAPI.Areas.Admin.Controllers.AdminPerson;

[ApiController]
[Area("Admin")]
[Route("api/[area]/AuthPerson")]
public class AdminAuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly TokenService _tokenService;

    public AdminAuthController(ApplicationDbContext context, TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }


    // POST: api/Admin/Person/Login
    [HttpPost("Login")]
    public async Task<ActionResult<PersonTokenDto>> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid) return BadRequest(loginDto);

        var person = await _context.Persons.SingleOrDefaultAsync(p => p.Email == loginDto.Email);

        if (person == null) return NotFound(new ErrorDto() {Error = "User not found"});

        var ps = new PasswordHasher<Person>();

        var isPassword = ps.VerifyHashedPassword(person, person.Password, loginDto.Password);

        if (isPassword != PasswordVerificationResult.Success)
            return Unauthorized(new ErrorDto {Error = "Have you forgotten your password?"});


        // Generate refresh token
        var refreshToken = TokenService.GenerateRandomBase64Url();
        person.RefreshToken = refreshToken;
        person.TokenExpire = DateTime.UtcNow.Add(TimeSpan.FromHours(5));

        try
        {
            _context.Persons.Update(person);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            return Problem(
                statusCode: 500,
                title: "Saving DB error",
                detail: e.InnerException?.Message
            );
        }


        return new PersonTokenDto()
        {
            Guid = person.Guid,
            AccessToken = _tokenService.GenerateJwtToken(person, out var expires),
            RefreshToken = person.RefreshToken,
            AccessTokenExpire = expires
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
    public async Task<ActionResult<PersonTokenDto>>
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

        // Generate refresh token
        var refreshToken = TokenService.GenerateRandomBase64Url();
        person.RefreshToken = refreshToken;
        person.TokenExpire = DateTime.UtcNow.Add(TimeSpan.FromHours(10));


        try
        {
            // throw new Exception("error");
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return new PersonTokenDto()
            {
                Guid = person.Guid,
                AccessToken = _tokenService.GenerateJwtToken(person, out var expires),
                RefreshToken = person.RefreshToken,
                AccessTokenExpire = expires
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