using System.ComponentModel.DataAnnotations;
namespace EventEase.Models;

public sealed class Registration
{
    public int EventId { get; set; }
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(80, MinimumLength = 2)]
    public string Name { get; set; } = "";
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    public string Email { get; set; } = "";
    public bool CheckedIn { get; set; }
}
