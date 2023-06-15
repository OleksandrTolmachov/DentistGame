using DentistBackend.Domain;
using DentistBackend.WebApi.Models;
using DentistBackend.WebApi.Services;
using System.Text;

namespace DentistBackend.WebApi.Repositories;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserCredentials?> Login(string username, string password)
    {
        var user = (await _unitOfWork.UserRepository.GetAllAsync()).Where(user => user.Username == username)
            .FirstOrDefault();

        if (user == null || !_passwordHasher.Verify(user.PasswordHash, password)) return null;

        var textBytes = Encoding.UTF8.GetBytes(string.Join(":", username, password));
        return new UserCredentials() { Username = username, Token = Convert.ToBase64String(textBytes) };
    }

    public async Task<UserCredentials> Register(string username, string password)
    {
        var playerStats = new PlayerStats() { Id = Guid.NewGuid(), FinishedLevels = 0 };
        await _unitOfWork.PlayerRepository.CreateAsync(playerStats);
        await _unitOfWork.UserRepository.CreateAsync(new User() { Id = Guid.NewGuid(), Username = username,
            PasswordHash = _passwordHasher.Hash(password), StatsId = playerStats.Id });
        await _unitOfWork.SaveAsync();

        var textBytes = Encoding.UTF8.GetBytes(string.Join(":", username, password));
        return new UserCredentials() { Username = username, Token = Convert.ToBase64String(textBytes) };
    }
}
