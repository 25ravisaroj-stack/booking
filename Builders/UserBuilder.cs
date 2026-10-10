using BookingSystemApi.Models;
namespace BookingSystemApi.Builders;
public class UserBuilder : IUserBuilder
{
    public User Build(UserRequest request)
    {
        return new User(
            request.FirstName,
            request.LastName,
            request.MobileNo,
            request.Email,
            request.Role,
            request.Password,
            request.RefreshToken,
            Convert.ToDateTime(request.RefreshTokenExpiryTime)
           
        );
    }
}