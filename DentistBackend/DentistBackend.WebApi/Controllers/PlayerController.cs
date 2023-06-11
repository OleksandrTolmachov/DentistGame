using DentistBackend.WebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DentistBackend.WebApi.Controllers;

[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class PlayerController : ControllerBase
{
    private readonly IPlayerService _playerService;

    public PlayerController(IPlayerService playerService)
    {
        _playerService = playerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPlayer()
    {
        return Ok(await _playerService.GetPlayerAsync(Guid.Parse(User.FindFirstValue("StatsId")!)));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPlayers()
    {
        return Ok(await _playerService.GetAllPlayersAsync());
    }

    [HttpPost]
    public async Task<IActionResult> PostFinishedLevel()
    {
        return Ok(await _playerService.RegisterFinishedLevelAsync(Guid.Parse(User.FindFirstValue("StatsId")!)));
    }
}
