
### An ASP.NET Core 7 Web API example with CRUD operations for hotels and bookings, applying async programming

## Notes
- Solution was created with Visual Studio for Mac , version 17.6.4 (2022)

- postgreSQL 16 server was set to run on port 5434 in dev environment

- the `Migrations` folder and its contents were created after running cmd
  `dotnet ef migrations add InitialCreate`
  in the terminal, from the project root folder


## Setup

- Clone repository
- Open solution with Visual Studio
- Install postgreSQL and run the server on any port desired. Accordingly set the value for "Port" in appsettings.Development.json:

`{
  "ConnectionStrings": {
    "WebApiDatabase": "Host=localhost; Port=5434; Database=postgres; Username=postgres"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}`

- Build solution
- Run `dotnet ef database update` in the terminal, from the project root folder, to migrate DB and create tables
- Run project and try out the api via the Swagger UI that loads on the browser

## Tests

- Project `HotelBookingsTests` includes all unit tests created with the xUnit framework
