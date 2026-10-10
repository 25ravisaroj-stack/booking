using BookingSystemApi.Models;
using BookingSystemApi.Data;
using Microsoft.EntityFrameworkCore;
namespace BookingSystemApi.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> AddAsync(User user)
    {
        await _context.Users.AddAsync(user);

        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users
        .FirstOrDefaultAsync(x => x.Email == email);
    }
    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

}