using BookingSystemApi.Models;
namespace BookingSystemApi.Builders;
public interface IUserBuilder
{
    User Build(UserRequest request);
}