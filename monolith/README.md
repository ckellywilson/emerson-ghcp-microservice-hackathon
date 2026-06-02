# Monolith Reference

This directory now contains the ContosoUniversity brownfield monolith source imported from:

- `ckellywilson/day-in-the-life-copilot-lab` (`main` branch)

Imported projects:

- `ContosoUniversity.sln`
- `ContosoUniversity.Core/`
- `ContosoUniversity.Infrastructure/`
- `ContosoUniversity.Web/`
- `ContosoUniversity.Tests/`
- `ContosoUniversity.PlaywrightTests/`

> Import excludes `*.bak` and `*.new` artifacts so the tree contains canonical source files only.

## Build and run

```bash
dotnet build monolith/ContosoUniversity.sln
dotnet run --project monolith/ContosoUniversity.Web
```

## Workshop extraction targets

### Day 1 showcase: Notification domain

- Interface seam: [`ContosoUniversity.Core/Interfaces/INotificationService.cs`](ContosoUniversity.Core/Interfaces/INotificationService.cs)
- Domain models: [`ContosoUniversity.Core/Models/Notification.cs`](ContosoUniversity.Core/Models/Notification.cs), [`ContosoUniversity.Core/Models/EntityOperation.cs`](ContosoUniversity.Core/Models/EntityOperation.cs)
- Infrastructure service: [`ContosoUniversity.Infrastructure/Services/ServiceBusNotificationService.cs`](ContosoUniversity.Infrastructure/Services/ServiceBusNotificationService.cs)
- Web endpoints: [`ContosoUniversity.Web/Controllers/NotificationController.cs`](ContosoUniversity.Web/Controllers/NotificationController.cs), [`ContosoUniversity.Web/Controllers/NotificationsController.cs`](ContosoUniversity.Web/Controllers/NotificationsController.cs)

### Day 2 hackathon: Student / Enrollment domain

- Student/Course/Enrollment models:
  - [`ContosoUniversity.Core/Models/Student.cs`](ContosoUniversity.Core/Models/Student.cs)
  - [`ContosoUniversity.Core/Models/Course.cs`](ContosoUniversity.Core/Models/Course.cs)
  - [`ContosoUniversity.Core/Models/Enrollment.cs`](ContosoUniversity.Core/Models/Enrollment.cs)
- Controllers:
  - [`ContosoUniversity.Web/Controllers/StudentsController.cs`](ContosoUniversity.Web/Controllers/StudentsController.cs)
  - [`ContosoUniversity.Web/Controllers/CoursesController.cs`](ContosoUniversity.Web/Controllers/CoursesController.cs)
