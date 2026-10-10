namespace BookingSystemApi.Models;

public class User
{
    public int id { get; set; }
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public long? MobileNo { get; private set; }
    public string? Email { get; private set; }
    public string? Password { get; private set; }
    public string? Role { get; private set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public User()
    {
    }
    public User(string? firstName, string? lastName, long? mobileNo, string Email, string Role, string password, string RefreshToken, DateTime RefreshTokenExpiryTime)
    {

        FirstName = firstName;
        LastName = lastName;
        MobileNo = mobileNo;
        this.Email = Email;
        this.Role = Role;
        this.Password = password;
        this.RefreshToken = RefreshToken;
        this.RefreshTokenExpiryTime = RefreshTokenExpiryTime;
    }
}