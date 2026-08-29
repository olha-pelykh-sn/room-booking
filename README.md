# Room Booking API
 
A REST API for booking meeting/conference rooms, built with **ASP.NET Core** and **Entity Framework Core**. It supports room management (CRUD) and booking creation with automatic cost calculation based on time-of-day tariffs.
 
## Features
 
- **Room management** — create, update, delete rooms, and search for rooms available in a given time slot and capacity.
- **Room booking** — book a room for a time range, with automatic conflict detection against existing bookings.
- **Time-of-day pricing** — rental cost is calculated per tariff zone, with the cost split proportionally when a booking spans multiple zones:
  | Zone      | Hours         | Rate            |
  |-----------|---------------|-----------------|
  | Morning   | 06:00 – 09:00 | −10% discount   |
  | Standard  | 09:00 – 12:00 | base price      |
  | Peak      | 12:00 – 14:00 | +15% surcharge  |
  | Standard  | 14:00 – 18:00 | base price      |
  | Evening   | 18:00 – 23:00 | −20% discount   |
- **Additional services** — bookings can include optional add-on services, each with its own price, summed into the total cost.
- **Validation** — request validation via FluentValidation.
- **Consistent error responses** — a global exception handler maps domain errors to RFC 7807 ProblemDetails responses.
- **Swagger / OpenAPI** — interactive API docs served at the app root in the Development environment.
## Tech Stack
 
- [ASP.NET Core](https://learn.microsoft.com/aspnet/core) (.NET 10)
- [Entity Framework Core](https://learn.microsoft.com/ef/core) with SQL Server
- [AutoMapper](https://automapper.org/) for entity ↔ DTO mapping
- [FluentValidation](https://docs.fluentvalidation.net/) for request validation
- [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) for Swagger/OpenAPI docs

  
