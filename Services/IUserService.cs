using BookingSystemApi.Logins;
using BookingSystemApi.Models;
namespace BookingSystemApi.Services;

public interface IUserService
{


    Task<User> CreateUser(UserRequest request);
    Task<List<User>> GetUsers();
    Task<LoginResponse?> LoginAsync(LoginRequest request);
    Task<LoginResponse?> RefreshTokenAsync(RefreshTokenRequest request);
}