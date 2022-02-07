using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace ConcertTicketAPI.Services;

public class TokenService
{
    
    /// <summary>
    ///     Generate Base64UrlEncoded token
    /// </summary>
    /// <param name="bytesLength">Bytes length. Default 32</param>
    /// <returns></returns>
    public static string GenerateBase64UrlToken(int bytesLength = 0x20)
    {
        var bytes = RandomNumberGenerator.GetBytes(bytesLength);
        return Base64UrlEncoder.Encode(bytes);
    }
}