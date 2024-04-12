# Cards RESTful web service

## Architecture
Onion Architecture
<img src="https://github.com/mikemathu/Cards/blob/master/AppData/Onion Architecture.PNG">

# Cards Web API

This project is a web API built using ASP.NET Core and follows the principles of Onion Architecture for better separation of concerns and maintainability.

## Class Libraries

### 1. Cards.Domain

- Contains the domain model of the application.
- **Directories**:
  - `Constants`: Definitions of constant values used within the app.
  - `Contracts`: Interfaces or abstract classes defining the contracts that `Repositories` in `Cards.Persistence` layer implement. Also acts as intermediary between `Cards.Domain` and `Cards.Services` layer.
  - `Entities`: Domain entities representing core business objects.
  - `ErrorModel`: Models representing error information.
  - `Exceptions`: Custom exception types.
  - `Shared`: Shared utility classes.

  ### 2. Cards.Services

- Contains business logic and application services.
- **Directories**:
  - `Abstraction`: Interfaces or abstract classes defining service contracts that `Services`. Also acts as intermediary between `Cards.Services` and `Cards.Presentation` layer.
  - `Dtos`: Data transfer objects used for transferring data between`Cards.Services` and `Cards.Presentation` layer.
  - `Services`: Implementations of `Abstraction` application services, containing the core business logic.
  - `MappingProfiles.cs`: AutoMapper mapping profiles for mapping between domain entities and DTOs.

  ### 3. Cards.Presentation

- Concerned with presentation logic, such as handling incoming requests and returning responses.
- **Directories**:
  - `ActionFilters`: Custom action filters for checking the model state of the incoming request.
  - `Controllers`: Responsible for handling incoming HTTP requests and invoking the appropriate services.

### 4. Cards.Persistence

- Responsible for data persistence concerns.
- **Directories**:
  - `Configurations`: Entity configurations for mapping domain entities to the database schema.
  - `Extensions`: Extension methods relevant to persistence operations.
  - `Migrations`: Database migration scripts for managing schema changes.
  - `Repositories`: Implementations of `Contracts` interfaces defined in the `Cards.Domain` layer.
  - `DbContext`: Database context class for interacting with the underlying database.

### 5. Cards.Web

- The outermost layer of the application, serving as the entry point for the web API.
- **Directories**:
  - `Extensions`: Extension methods for configuring services in the application.
  - `GlobalExceptionHandler.cs`: Global exception handling middleware for handling exceptions in the application.
  - `Program.cs`: The entry point class for configuring and running the application.



## How to Run

### Installation

    clone and open the solution file in Visual Studio

### Configure connection string

Set a database connection string,`DefaultConnection`, in the **Cards.Web** project's appsettings.json or use [Secrets](https://blogs.msdn.microsoft.com/mihansen/2017/09/10/managing-secrets-in-net-core-2-0-apps/)

Example config setting in appsettings.json for a database called `Cards`:

```json
   "ConnectionStrings": {
    "DefaultConnection": "Host=localhost; Database=Cards;  Username=postgres; Password=yourpassword"
  }
```
*"yourpassword"* - password to your database

### Create database
Method 1
- Open Package Manager Console in visual studio.
- Select the "Cards.Persistence" project from the dropdown as shown below.
- Run command "update-database" to create the database and seed the data.

<img src="https://github.com/mikemathu/Cards/blob/master/AppData/Cards_Persistence.PNG">

Method 2

- Use pgAdmin to create an empty database named **Cards**. 
- Import the **Cards.sql** file from the **AppData** directory of this project.



### Configure startup in IDE

- Set the Startup Item in your IDE to **https** and start the server.

### Configure an API client for interacting with the API

- Open Insomnia (or another tool) and import the **Cards.json** file from the 'AppData' directory of this project.
