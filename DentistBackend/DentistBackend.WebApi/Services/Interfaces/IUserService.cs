using DentistBackend.WebApi.Models;

namespace DentistBackend.WebApi.Repositories;

public interface IUserService
{
    public Task<UserCredentials?> Login(string username, string password);
    public Task<UserCredentials> Register(string username, string password);
}
