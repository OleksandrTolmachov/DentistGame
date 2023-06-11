using DentistBackend.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace DentistBackend.WebApi.Models;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;

    [ForeignKey(nameof(StatsId))]
    public PlayerStats Stats { get; set; }

    public Guid StatsId { get; set; }
}
