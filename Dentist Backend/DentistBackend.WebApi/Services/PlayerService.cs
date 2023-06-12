using DentistBackend.Domain;
using DentistBackend.WebApi.Models;
using DentistBackend.WebApi.PlayerDbContext;
using Microsoft.EntityFrameworkCore;

namespace DentistBackend.WebApi.Repositories;

public class PlayerService : IPlayerService
{
    private readonly GameDbContext _playerContext;

    public PlayerService(GameDbContext playerContext)
    {
        _playerContext = playerContext;
    }

    public async Task<IEnumerable<PlayerDto>> GetAllPlayersAsync()
    {
        var playerStats = await _playerContext.PlayerStats.ToListAsync();
        var users = await _playerContext.Users.ToListAsync();

        var playerDtos = playerStats
            .Join(users,
            stats => stats.Id,
            user => user.StatsId,
            (stats, user) => new PlayerDto
            {
                Id = stats.Id,
                FinishedLevels = stats.FinishedLevels,
                Username = user.Username
            })
            .ToList();

        return playerDtos;
    }

    public async Task<PlayerDto> GetPlayerAsync(Guid id)
    {
        PlayerStats? player = await _playerContext.PlayerStats
            .FirstOrDefaultAsync(player => player.Id == id)
            ?? throw new ArgumentException($"Invalid {nameof(id)}.");

        User user = await _playerContext.Users.FirstOrDefaultAsync(user => user.StatsId == player.Id)
              ?? throw new ArgumentException($"Invalid {nameof(id)}."); ;

        return new PlayerDto() {Id = player.Id, Username = user.Username, FinishedLevels = player.FinishedLevels};
    }

    public async Task<bool> RegisterFinishedLevelAsync(Guid id)
    {
        PlayerStats? player = await _playerContext.PlayerStats
            .FirstOrDefaultAsync(player => player.Id == id);

        if (player is null) return false;
        player.FinishedLevels++;
        await _playerContext.SaveChangesAsync();
        return true;
    }
}
