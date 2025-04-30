using System.ComponentModel.DataAnnotations;

namespace Core;

public class Weight
{
    [Required]
    public int UserId { get; set; }
    [Required]
    public double WeightKg { get; set; }
    [Required]
    public double WeightLbs { get; set; }
    [Required]
    public DateOnly Date { get; set; }
}