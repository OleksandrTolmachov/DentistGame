using DentistBackend.Domain;
using DentistBackend.WebApi.Models;
using DentistBackend.WebApi.Repositories.Interfaces;

namespace DentistBackend.WebApi.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<PlayerStats> PlayerRepository { get; set; }
        IRepository<User> UserRepository { get; set; }

        Task SaveAsync();
    }
}