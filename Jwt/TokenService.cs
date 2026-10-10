
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BookingSystemApi.Models;


namespace BookingSystemApi.Jwt;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateAccessToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier,user.id.ToString()),

           new Claim(ClaimTypes.Name, user.FirstName ?? ""),
            new Claim(ClaimTypes.Email,user.Email ?? ""),
            new Claim(ClaimTypes.Role,user.Role ?? "")
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        var credentials =
            new SigningCredentials(key,
                SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];

        using var rng = RandomNumberGenerator.Create();

        rng.GetBytes(randomNumber);
        var refreshTokenHash = HashRefreshToken(Convert.ToBase64String(randomNumber));
        return refreshTokenHash;
        // return Convert.ToBase64String(randomNumber);
    }
    public string HashRefreshToken(string refreshToken)
    {
        using var sha256 = SHA256.Create();

        var bytes = Encoding.UTF8.GetBytes(refreshToken);

        var hash = sha256.ComputeHash(bytes);

        return Convert.ToBase64String(hash);
    }
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false,

                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            _configuration["Jwt:Key"]))
            };

        var tokenHandler =
            new JwtSecurityTokenHandler();

        var principal =
            tokenHandler.ValidateToken(
                token,
                tokenValidationParameters,
                out SecurityToken securityToken);

        return principal;
    }
}