using System.ComponentModel.DataAnnotations;

namespace Core;

public class User
{
    [Required]
    public int Id { get; set; }
    [Required]
    [Length(8, 20, ErrorMessage = "Password must be between 8 and 20 characters")]
    public string Password { get; set; }
    [Required]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required] 
    public char Gender { get; set; } = 'M';
    [Required]
    public DateOnly Birthday { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    [Required]
    [Length(2, 20, ErrorMessage = "First name must be between 2 and 20 characters")]
    [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only characters from the alphabet are allowed.")]
    public string FirstName { get; set; }
    [Required]
    [Length(2, 20, ErrorMessage = "Last name must be between 2 and 20 characters")]
    [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only characters from the alphabet are allowed.")]
    public string LastName { get; set; }
}