using BookingSystemApi.Models;
namespace BookingSystemApi.Repositories;

public interface IUserRepository
{
    Task<User> AddAsync(User user);
    Task<List<User>> GetAllAsync();
    Task<User?> GetUserByEmailAsync(string email);
    Task UpdateAsync(User user);
}