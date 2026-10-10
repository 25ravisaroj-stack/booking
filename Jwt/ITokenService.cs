using System.Security.Claims;
using BookingSystemApi.Models;
namespace BookingSystemApi.Jwt;

public interface ITokenService
{
    string GenerateAccessToken(User user);

    string GenerateRefreshToken();

    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}