namespace EventEase.Services;

// Scoped to a Blazor Server circuit, not an authentication system.
// Refresh/reconnect or server restart may reset this state.
public sealed class SessionService
{
    public string? DisplayName { get; private set; }
    public string? Email { get; private set; }
    public int Actions { get; private set; }
    public void Identify(string name, string email) { DisplayName = name; Email = email; Actions++; }
    public void RecordAction() => Actions++;
    public void Clear() { DisplayName = null; Email = null; Actions = 0; }
}
