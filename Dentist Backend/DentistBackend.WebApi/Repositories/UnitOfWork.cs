using DentistBackend.Domain;
using DentistBackend.WebApi.Models;
using DentistBackend.WebApi.PlayerDbContext;
using DentistBackend.WebApi.Repositories.Interfaces;

namespace DentistBackend.WebApi.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public GameDbContext _context;

    public UnitOfWork(GameDbContext context,
        IRepository<User> userRepository,
        IRepository<PlayerStats> playerRepository)
    {
        _context = context;
        UserRepository = userRepository;
        PlayerRepository = playerRepository;
    }

    public IRepository<User> UserRepository { get; set; }
    public IRepository<PlayerStats> PlayerRepository { get; set; }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
