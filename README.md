# EventEase

EventEase is my Blazor coursework project: a small event management app where visitors can browse events, register, and check attendance. I built it to practise reusable components, routing, validation, and managing state in C#.

## What it does

- Shows event cards with a name, date and location.
- Opens event details and registration pages through Blazor routes. Unknown events and unknown pages receive a helpful message.
- Includes an event editor with a live preview using `@bind-Event` and input validation.
- Validates registration names and email addresses, and prevents repeat registration with the same email for the same event.
- Tracks registered participants and allows check-in and check-out.
- Shows session information for the current connected user.
- Uses `Virtualize` to efficiently render a larger demonstration list of 1,000 events.

The app uses **.NET 8 Blazor Web App with interactive server rendering**. Data is held in memory, so registrations and edits are lost when the server restarts. Session information is stored in a scoped service per Blazor circuit; it's a demonstration of state management, **not authentication** or persistent login. Attendance information is accessible in this demo without access controls. I would add authentication, authorization and persistent storage before any real-world deployment.

## Run locally

Install the [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) and open this folder in VS Code (the C# Dev Kit extension is helpful). In the terminal run:

```bash
dotnet restore
dotnet run
```

Open the local URL printed by the terminal (the included HTTP launch profile uses `http://localhost:5197`). The site needs the running ASP.NET Core server; opening an HTML file directly will not run it.

## Things to try

1. Browse events and search for a location.
2. Open the **Event editor**, change a field and watch the preview update; try blank text or a past date before saving.
3. Register for an event, then try registering a second time with the same email.
4. Open **Attendance**, select the event and check a participant in and out.
5. Open **My session** to see the last registered name and email.
6. Toggle the large sample list on **Events** and scroll to see virtualization.
7. Try an unknown URL or `/events/9999` to check the error paths.


Replace `YOUR_USERNAME` with your own GitHub username. You may be asked to sign in to GitHub when pushing. Submit the **repository URL** (`https://github.com/YOUR_USERNAME/EventEase`) to Coursera. GitHub Pages alone cannot host a server-interactive Blazor app; publishing the source repo is enough for this assignment.


## Limitations

The demo has sample data rather than a database, doesn't send confirmation emails, and has no user accounts or organizer permissions. The large sample list contains synthetic preview events that cannot be registered for. This project is intended to show the coursework concepts, not to operate a real ticketing service.
