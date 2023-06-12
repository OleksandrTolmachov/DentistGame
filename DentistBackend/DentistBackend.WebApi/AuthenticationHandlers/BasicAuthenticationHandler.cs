using DentistBackend.WebApi.PlayerDbContext;
using DentistBackend.WebApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace DentistBackend.WebApi.Handlers;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly GameDbContext _gameDbContext;

    public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory loggerFactory,
        UrlEncoder urlEncoder,
        ISystemClock systemClock,
        IPasswordHasher passwordHasher,
        GameDbContext gameDbContext) : base(options, loggerFactory, urlEncoder, systemClock)
    {
        _passwordHasher = passwordHasher;
        _gameDbContext = gameDbContext;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
            return AuthenticateResult.Fail("No authorization header was provided.");

        try
        {
            var authenticationHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            byte[] bytes = Convert.FromBase64String(authenticationHeader.Parameter);
            string[] credentials = Encoding.UTF8.GetString(bytes).Split(":");
            string username = credentials[0];
            string password = credentials[1];

            var user = await _gameDbContext.Users.Where(user => user.Username == username)
                .FirstOrDefaultAsync();

            if (user is null || !_passwordHasher.Verify(user.PasswordHash, password))
            {
                return AuthenticateResult.Fail("Invalid name or password.");
            }

            var claims = new[] { new Claim(ClaimTypes.Name, username), 
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("StatsId", user.StatsId.ToString())};
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
        catch (Exception) 
        {
            return AuthenticateResult.Fail("Authentication error has occured.");
        }
    }
}
