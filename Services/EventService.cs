using EventEase.Models;
namespace EventEase.Services;

// Shared, in-memory coursework data. Lock protects concurrent users/circuits.
public sealed class EventService
{
    private readonly object gate = new();
    private readonly List<EventModel> events = new()
    {
        new() { Id = 1, Name = "Developer Meetup", Date = new DateTime(2026, 11, 15), Location = "Geneva", Description = "An informal evening for developers to meet and exchange ideas." },
        new() { Id = 2, Name = "Blazor Workshop", Date = new DateTime(2026, 11, 22), Location = "Online", Description = "A practical introduction to interactive Blazor components." },
        new() { Id = 3, Name = "Fintech Roundtable", Date = new DateTime(2026, 12, 5), Location = "Lausanne", Description = "Discussing software challenges in financial services." }
    };
    private readonly List<Registration> registrations = new();

    public IReadOnlyList<EventModel> GetEvents()
    {
        lock (gate) return events.Select(e => e.Copy()).ToList();
    }

    public EventModel? GetEvent(int id)
    {
        lock (gate) return events.FirstOrDefault(e => e.Id == id)?.Copy();
    }

    public bool UpdateEvent(EventModel updated, out string error)
    {
        error = "";
        if (string.IsNullOrWhiteSpace(updated.Name) || updated.Name.Trim().Length < 3 ||
            string.IsNullOrWhiteSpace(updated.Location) || updated.Location.Trim().Length < 2 ||
            updated.Date.Date < DateTime.Today)
        {
            error = "Please enter a valid name, location and a date that is not in the past.";
            return false;
        }
        lock (gate)
        {
            var current = events.FirstOrDefault(e => e.Id == updated.Id);
            if (current is null) { error = "Event not found."; return false; }
            current.Name = updated.Name.Trim();
            current.Location = updated.Location.Trim();
            current.Date = updated.Date;
            return true;
        }
    }

    public bool Register(int eventId, string name, string email, out string error)
    {
        error = "";
        name = name.Trim(); email = email.Trim();
        lock (gate)
        {
            if (!events.Any(e => e.Id == eventId)) { error = "Event not found."; return false; }
            if (registrations.Any(r => r.EventId == eventId && string.Equals(r.Email, email, StringComparison.OrdinalIgnoreCase)))
            { error = "This email is already registered for the event."; return false; }
            registrations.Add(new Registration { EventId = eventId, Name = name, Email = email });
            return true;
        }
    }

    public IReadOnlyList<Registration> GetRegistrations(int eventId)
    {
        lock (gate) return registrations.Where(r => r.EventId == eventId)
            .Select(r => new Registration { EventId = r.EventId, Name = r.Name, Email = r.Email, CheckedIn = r.CheckedIn }).ToList();
    }

    public bool SetCheckedIn(int eventId, string email, bool checkedIn)
    {
        lock (gate)
        {
            var r = registrations.FirstOrDefault(x => x.EventId == eventId && string.Equals(x.Email, email, StringComparison.OrdinalIgnoreCase));
            if (r is null) return false;
            r.CheckedIn = checkedIn;
            return true;
        }
    }
}
