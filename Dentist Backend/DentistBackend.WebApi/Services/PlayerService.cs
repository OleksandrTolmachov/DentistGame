using DentistBackend.Domain;
using DentistBackend.WebApi.Models;
using DentistBackend.WebApi.Repositories.Interfaces;

namespace DentistBackend.WebApi.Repositories;

public class PlayerService : IPlayerService
{
    private readonly IRepository<PlayerStats> _playerRepository;
    private readonly IRepository<User> _userRepository;

    public PlayerService(IRepository<PlayerStats> playerRepository, IRepository<User> userRepository)
    {
        _playerRepository = playerRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<PlayerDto>> GetAllPlayersAsync()
    {
        var playerStats = await _playerRepository.GetAllAsync();
        var users = await _userRepository.GetAllAsync();

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
        PlayerStats? player = await _playerRepository.GetByIdAsync(id)
            ?? throw new ArgumentException($"Invalid {nameof(id)}.");

        User user = (await _userRepository.GetAllAsync()).FirstOrDefault(user => user.StatsId == player.Id)
              ?? throw new ArgumentException($"Invalid {nameof(id)}."); ;

        return new PlayerDto() {Id = player.Id, Username = user.Username, FinishedLevels = player.FinishedLevels};
    }

    public async Task<bool> RegisterFinishedLevelAsync(Guid id)
    {
        PlayerStats? player = await _playerRepository.GetByIdAsync(id);

        if (player is null) return false;
        player.FinishedLevels++;
        await _playerRepository.UpdateAsync(player);
        return true;
    }
}
