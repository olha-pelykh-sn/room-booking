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

## Getting Started
 
To get a local copy of this project up and running, follow these steps.
 
### Prerequisites
 
- .NET 10 SDK
- A SQL Server instance (local, container, or Azure SQL)
- Entity Framework Core tools: `dotnet tool install --global dotnet-ef`
### Installation
 
1. Clone the repository:
```bash
   git clone https://github.com/olha-pelykh-sn/room-booking.git
   cd room-booking/room-booking-backend
```
 
2. Restore dependencies:
```bash
   dotnet restore
```
 
3. Set up the connection string:
   Set the database connection string using user-secrets instead of editing `appsettings.json` directly:
```bash
   dotnet user-secrets init
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "<your-connection-string>"
```

4. Run database migrations:
   Ensure your database is reachable and then run:
```bash
   dotnet ef database update
```
 
5. Start the development server:
```bash
   dotnet run
```
 
## Usage
 
### Running the API
 
- **Development mode:** `dotnet run` (uses `ASPNETCORE_ENVIRONMENT=Development`, enables Swagger UI)
- **Production mode:** `dotnet publish -c Release` followed by running the published output, or `dotnet run --environment Production`
Open `http://localhost:5278` (or `https://localhost:7146`) to reach the API.
 
### API Documentation
The API documentation for this application is available at `http://localhost:5278` (Swagger UI, served at the root in Development). It details all endpoints, request/response schemas, and lets you try requests directly from the browser.
 
#### Rooms — `/api/rooms`
 
| Method | Route               | Description                                      |
|--------|----------------------|---------------------------------------------------|
| GET    | `/available`         | List rooms available for a check-in/check-out window and minimum capacity |
| POST   | `/`                   | Create a new room (name must be unique)           |
| PUT    | `/{id}`               | Update an existing room                           |
| DELETE | `/{id}`               | Delete a room                                     |
 
#### Bookings — `/api/bookings`
 
| Method | Route | Description |
|--------|-------|-------------|
| POST   | `/`   | Create a booking for a room, given a start time, duration (minutes), and optional service IDs. Returns a cost breakdown (rental cost, services cost, total). |

## Contributing
 
We welcome contributions to this project. Please follow these steps to contribute:
 
1. Clone the repository.
2. Create a new branch (`git checkout -b feature/your-feature-name`).
3. Make your changes and commit them (`git commit -m 'Add some feature'`).
4. Push to the branch (`git push origin feature/your-feature-name`).
5. Open a pull request.
Please make sure to update tests as appropriate.
 
If a code of conduct is added to this repository, all contributors are expected to follow it to keep the project welcoming and inclusive.
 
## Issues
 
If you encounter any issues while using or setting up the project, please check the [Issues](https://github.com/olha-pelykh-sn/room-booking/issues) section to see if it has already been reported. If not, feel free to open a new issue detailing the problem.
 
When reporting an issue, please include:
 
- A clear and descriptive title.
- A detailed description of the problem.
- Steps to reproduce the issue.
- Any relevant logs or screenshots.
- The environment in which the issue occurs (OS, .NET SDK version, database provider, etc.).

## License
 
No license file is currently included in this repository.
