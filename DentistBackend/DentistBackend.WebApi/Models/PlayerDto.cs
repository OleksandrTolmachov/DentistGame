namespace DentistBackend.WebApi.Models;

public class PlayerDto
{
    public Guid Id { get; set; }
    public int FinishedLevels { get; set; }
    public string Username { get; set; }
}