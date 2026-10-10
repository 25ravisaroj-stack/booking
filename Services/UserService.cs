using BookingSystemApi.Models;
using BookingSystemApi.Repositories;
using BookingSystemApi.Builders;
using BookingSystemApi.Logins;
using BookingSystemApi.Jwt;
using System.Security.Claims;
namespace BookingSystemApi.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IUserBuilder _builder;
    private readonly ITokenService _tokenservice;

    public UserService(
        IUserRepository repository,
        IUserBuilder builder, ITokenService tokenService)
    {
        _repository = repository;
        _builder = builder;
        _tokenservice = tokenService;
    }

    public async Task<User> CreateUser(UserRequest request)
    {
        var user = _builder.Build(request);

        await _repository.AddAsync(user);

        return user;
    }

    public async Task<List<User>> GetUsers()
    {
        return await _repository.GetAllAsync();
    }
    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var user = await _repository
        .GetUserByEmailAsync(request.Email);

        if (user == null)
            return null;

        if (user.Password != request.Password)
            return null;

        var AccessToken = _tokenservice.GenerateAccessToken(user);
        var RefreshToken = _tokenservice.GenerateRefreshToken();
        user.RefreshToken = RefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _repository.UpdateAsync(user);
        return new LoginResponse
        {
            AccessToken = AccessToken,
            RefreshToken = RefreshToken,

        };
    }
    public async Task<LoginResponse?> RefreshTokenAsync(
    RefreshTokenRequest request)
    {
        var principal =
        _tokenservice.GetPrincipalFromExpiredToken(
        request.AccessToken);
var email = principal.FindFirst(ClaimTypes.Email)?.Value;
        //var email = principal.Identity?.Name;

        if (string.IsNullOrEmpty(email))
            return null;

        var user = await _repository
        .GetUserByEmailAsync(email);

        if (user == null)
            return null;

        if (user.RefreshToken != request.RefreshToken)
            return null;

        if (user.RefreshTokenExpiryTime < DateTime.UtcNow)
            return null;

        var newAccessToken =
        _tokenservice.GenerateAccessToken(user);

        var newRefreshToken =
        _tokenservice.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;

        await _repository.UpdateAsync(user);

        return new LoginResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }
}