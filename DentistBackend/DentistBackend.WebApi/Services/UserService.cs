using DentistBackend.Domain;
using DentistBackend.WebApi.Models;
using DentistBackend.WebApi.PlayerDbContext;
using DentistBackend.WebApi.Services;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DentistBackend.WebApi.Repositories;

public class UserService : IUserService
{
    private readonly GameDbContext _gameContext;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(GameDbContext gameContext, IPasswordHasher passwordHasher)
    {
        _gameContext = gameContext;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserCredentials?> Login(string username, string password)
    {
        var user = await _gameContext.Users.Where(user => user.Username == username)
            .FirstOrDefaultAsync();

        if (user == null || !_passwordHasher.Verify(user.PasswordHash, password)) return null;

        var textBytes = Encoding.UTF8.GetBytes(string.Join(":", username, password));
        return new UserCredentials() { Username = username, Token = Convert.ToBase64String(textBytes) };
    }

    public async Task<UserCredentials> Register(string username, string password)
    {
        var playerStats = new PlayerStats() { Id = Guid.NewGuid(), FinishedLevels = 0 };
        await _gameContext.PlayerStats.AddAsync(playerStats);
        await _gameContext.Users.AddAsync(new User() { Id = Guid.NewGuid(), Username = username,
            PasswordHash = _passwordHasher.Hash(password), StatsId = playerStats.Id });
        await _gameContext.SaveChangesAsync();

        var textBytes = Encoding.UTF8.GetBytes(string.Join(":", username, password));
        return new UserCredentials() { Username = username, Token = Convert.ToBase64String(textBytes) };
    }
}
