using Microsoft.EntityFrameworkCore;
using BookingSystemApi.Models;
namespace BookingSystemApi.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}
