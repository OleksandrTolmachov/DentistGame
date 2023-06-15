using DentistBackend.Domain;
using DentistBackend.WebApi.Models;
using DentistBackend.WebApi.Repositories.Interfaces;

namespace DentistBackend.WebApi.Repositories;

public class PlayerService : IPlayerService
{
    private readonly IUnitOfWork _unitOfWork;

    public PlayerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<PlayerDto>> GetAllPlayersAsync()
    {
        var playerStats = await _unitOfWork.PlayerRepository.GetAllAsync();
        var users = await _unitOfWork.UserRepository.GetAllAsync();

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
        PlayerStats? player = await _unitOfWork.PlayerRepository.GetByIdAsync(id)
            ?? throw new ArgumentException($"Invalid {nameof(id)}.");

        User user = (await _unitOfWork.UserRepository.GetAllAsync()).FirstOrDefault(user => user.StatsId == player.Id)
              ?? throw new ArgumentException($"Invalid {nameof(id)}."); ;

        return new PlayerDto() {Id = player.Id, Username = user.Username, FinishedLevels = player.FinishedLevels};
    }

    public async Task<bool> RegisterFinishedLevelAsync(Guid id)
    {
        PlayerStats? player = await _unitOfWork.PlayerRepository.GetByIdAsync(id);

        if (player is null) return false;
        player.FinishedLevels++;
        await _unitOfWork.PlayerRepository.UpdateAsync(player);
        await _unitOfWork.PlayerRepository.SaveAsync();
        await _unitOfWork.SaveAsync();
        return true;
    }
}
