using DentistBackend.WebApi.Models;
using DentistBackend.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DentistBackend.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] UserLogin userDto)
    {
        var userResponse = await _userService.Login(userDto.Username, userDto.Password);
        return Ok(userResponse);
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserRegister userDto)
    {
        var userResponse = await _userService.Register(userDto.Username, userDto.Password);
        return Ok(userResponse);
    }
}
