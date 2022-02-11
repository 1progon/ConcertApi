using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ConcertTicketAPI.Models.Person;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;

namespace ConcertTicketAPI.Services;

public class TokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration) => _configuration = configuration;

    /// <summary>
    /// Generate Base64UrlEncoded token
    /// </summary>
    /// <param name="bytesLength">Bytes length. Default 32</param>
    /// <returns></returns>
    public static string GenerateRandomBase64Url(int bytesLength = 0x20)
    {
        var bytes = RandomNumberGenerator.GetBytes(bytesLength);
        return Base64UrlEncoder.Encode(bytes);
    }


    /// <summary>
    /// Generate Base64URl string from text string
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static string CreateBase64Url(string text = "")
    {
        var bytes = Encoding.UTF8.GetBytes(text);
        return Base64UrlTextEncoder.Encode(bytes);
    }

    /// <summary>
    /// Generate JWT string token with claims based on Person person
    /// </summary>
    /// <param name="person"></param>
    /// <param name="expires"></param>
    /// <param name="expireMinutes"></param>
    /// <returns></returns>
    public string GenerateJwtToken(Person person, out DateTime expires, double expireMinutes = 5)
    {
        // generate jwt token with claims
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, person.Guid.ToString()),
        };

        expires = DateTime.UtcNow.Add(TimeSpan.FromMinutes(expireMinutes));


        var token = new JwtSecurityToken(
            _configuration.GetSection("Jwt")["Issuer"],
            _configuration.GetSection("Jwt")["Audience"],
            claims,
            expires: expires,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt")
                        ["Key"])),
                SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}