using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace DentistBackend.WebApi.Services;

public class PasswordHasherOptions
{
    public int SaltSize { get; set; }
    public int KeySize { get; set; }
    public int Iterations { get; set; }
    public char Delemitor { get; set; }
}

public class PasswordHasher : IPasswordHasher
{
    //private const int SaltSize = 128 / 8;
    //private const int KeySize = 256 / 8;
    //private const int Iterations = 10_000;
    //private const char Delemitor = ';';
    private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;
    private readonly IOptions<PasswordHasherOptions> _options;

    public PasswordHasher(IOptions<PasswordHasherOptions> options)
    {
        _options = options;
    }

    public string Hash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(_options.Value.SaltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, _options.Value.Iterations,
            _hashAlgorithmName, _options.Value.KeySize);

        return string.Join(_options.Value.Delemitor, Convert.ToBase64String(salt), 
            Convert.ToBase64String(hash));
    }

    public bool Verify(string passwordHash, string inputPassword)
    {
        string[] elements = passwordHash.Split(_options.Value.Delemitor);
        byte[] salt = Convert.FromBase64String(elements[0]);
        byte[] hash = Convert.FromBase64String(elements[1]);

        byte[] hashInput = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, _options.Value.Iterations,
            _hashAlgorithmName, _options.Value.KeySize);

        return CryptographicOperations.FixedTimeEquals(hash, hashInput);
    }
}
