using BookingSystemApi.Services;
using Microsoft.AspNetCore.Mvc;
namespace BookingSystemApi.Models;


[ApiController]   
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> SaveUserDetails(
        UserRequest request)
    {
        var user = await _service.CreateUser(request);

        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _service.GetUsers();

        return Ok(users);
    }
}
