using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ConcertTicketAPI;

public class TestDevAuthOptions
{
    public const string Issuer = "MyAuthServer";
    public const string Audience = "MyAuthClient";
    private const string Key = "ewfefewfwefewfewfew";

    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new(Encoding.UTF8.GetBytes(Key));
}