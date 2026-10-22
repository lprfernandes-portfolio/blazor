using System.ComponentModel.DataAnnotations;
namespace EventEase.Models;

public sealed class EventModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Please enter an event name.")]
    [StringLength(80, MinimumLength = 3)]
    public string Name { get; set; } = "";
    [Required(ErrorMessage = "Please enter a location.")]
    [StringLength(100, MinimumLength = 2)]
    public string Location { get; set; } = "";
    public DateTime Date { get; set; } = DateTime.Today.AddDays(7);
    public string Description { get; set; } = "";

    public EventModel Copy() => new() { Id = Id, Name = Name, Location = Location, Date = Date, Description = Description };
}
