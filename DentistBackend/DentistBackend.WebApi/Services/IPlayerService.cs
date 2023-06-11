using DentistBackend.Domain;
using DentistBackend.WebApi.Models;

namespace DentistBackend.WebApi.Repositories;

public interface IPlayerService
{
    public Task<PlayerDto> GetPlayerAsync(Guid id);

    public Task<IEnumerable<PlayerDto>> GetAllPlayersAsync();

    public Task<bool> RegisterFinishedLevelAsync(Guid id);
}
