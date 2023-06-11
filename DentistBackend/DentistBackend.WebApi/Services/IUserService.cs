using DentistBackend.Domain;

namespace DentistBackend.WebApi.Repositories;

public interface IUserService
{
    public Task<UserResponse?> Login(string username, string password);
    public Task<UserResponse> Register(string username, string password);
}
