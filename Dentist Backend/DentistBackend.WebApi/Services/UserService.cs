using DentistBackend.Domain;
using DentistBackend.WebApi.Models;
using DentistBackend.WebApi.Repositories.Interfaces;
using DentistBackend.WebApi.Services;
using System.Text;

namespace DentistBackend.WebApi.Repositories;

public class UserService : IUserService
{
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<PlayerStats> _playerRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(IRepository<User> userRepository, IPasswordHasher passwordHasher,
        IRepository<PlayerStats> playerRepository)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _playerRepository = playerRepository;
    }

    public async Task<UserCredentials?> Login(string username, string password)
    {
        var user = (await _userRepository.GetAllAsync()).Where(user => user.Username == username)
            .FirstOrDefault();

        if (user == null || !_passwordHasher.Verify(user.PasswordHash, password)) return null;

        var textBytes = Encoding.UTF8.GetBytes(string.Join(":", username, password));
        return new UserCredentials() { Username = username, Token = Convert.ToBase64String(textBytes) };
    }

    public async Task<UserCredentials> Register(string username, string password)
    {
        var playerStats = new PlayerStats() { Id = Guid.NewGuid(), FinishedLevels = 0 };
        await _playerRepository.CreateAsync(playerStats);
        await _userRepository.CreateAsync(new User() { Id = Guid.NewGuid(), Username = username,
            PasswordHash = _passwordHasher.Hash(password), StatsId = playerStats.Id });

        var textBytes = Encoding.UTF8.GetBytes(string.Join(":", username, password));
        return new UserCredentials() { Username = username, Token = Convert.ToBase64String(textBytes) };
    }
}
