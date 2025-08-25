using System.ComponentModel.DataAnnotations;

public class UserCreationDto
{
    [EmailAddress]
    public string Email { get; set; } = string.Empty;    public string Password { get; set; } = string.Empty;
    public string? Role { get; set; }
}