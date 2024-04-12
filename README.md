# Cards RESTful web API service

## Architecture

This project is a web API built using ASP.NET Core and follows the principles of Onion Architecture for better separation of concerns and maintainability.
Onion Architecture
<img src="https://github.com/mikemathu/Cards/blob/master/AppData/onion%20architecture.PNG">
<img src="https://github.com/mikemathu/Cards/blob/master/AppData/code%20architecture.PNG">

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

## Technology Stack

- Frontend: HTML, CSS, Bootstrap 5, JavaScript
- Backend: ASP.NET Core Web API with C#
- Database: [PostgreSQL](https://www.postgresql.org/download/)
- Data Access: [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core)
- Platform: [.NET 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) 
- API Tests: [Insomnia](https://insomnia.rest/download)


## How to setup and Run

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
- Select the `Cards.Persistence` project from the dropdown as shown below.
- Run command "update-database" to create the database and seed the data.

<img src="https://github.com/mikemathu/Cards/blob/master/AppData/Cards_Persistence.PNG">

Method 2

- Use pgAdmin to create an empty database named **Cards**. 
- Import the **[Cards.sql](https://github.com/mikemathu/Cards/blob/master/AppData/Cards.sql)** file from the **AppData** directory of this project.



### Configure startup in IDE

- Set the Startup Item in your IDE to **https** and start the server.

### Configure an API client for interacting with the API
-- Open Insomnia (or another tool) and import the **[Cards.json](https://github.com/mikemathu/Cards/blob/master/AppData/Cards.json)** file from the `AppData` directory of this project.
- [Postman Collection](https://github.com/mikemathu/Cards/blob/master/AppData/Cards.json)


## Authentication Endpoints

 ### Register
|              |                                      |
|--------------| -------------------------------------|
| HTTP Method  | POST                                 |
| End-point    | {{baseUrl}}/api/authentication/      |

### Register Parameters
| Field                              | Position | Data Type | Required | Description      |
|------------------------------------|----------|-----------|----------|------------------|
| Content-Type = Application/json    | Header   |           | Yes      | -                |
| email                              | body     | string    | Yes      | User email.      |
| password                           | body     | string    | Yes      | User password.   |
 
 ### Sample Request
 ```json
{
    "email": "testuser@gmail.com",
    "password": "testuserP@ssorwd1"
}
 ```

 ### Login
|              |                                      |
|--------------| -------------------------------------|
| HTTP Method  | POST                                 |
| End-point    | {{baseUrl}}/api/authentication/login |

### Login Parameters
| Field                              | Position | Data Type | Required | Description      |
|------------------------------------|----------|-----------|----------|------------------|
| Content-Type = Application/json    | Header   |           | Yes      |                  |
| email                              | Body     | String    | Yes      | User email.      |
| password                           | Body     | String    | Yes      | User password.   |
 
 ### Sample Request
 ```json
{
    "email": "kev@gmail.com",
    "password": "kevP@ssword1"
}
 ```
 
 ### Sample Response
 ```json
{
    	"token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJrZXZAZ21haWwuY29tIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiTWVtYmVyIiwiaXNzIjoiQ2FyZHNBUEkiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo3MjY1In0.o4WBWXaJdS9xhhRmSwaY0wwuMo40J1A-oSi4WjZZnZE"
}
 ```



## Manage Card Endpoints

 ### Add a Card
|                  |                                                    |
|------------------|----------------------------------------------------|
| HTTP Method      | POST                                               |
| End-point        | {{baseUrl}}/api/appUsers/{appUserId}/cards/        |

### Add a Card Parameters
|                                  | Position       | Data Type | Required | Description                                                  |
|----------------------------------|----------------|-----------|----------|--------------------------------------------------------------|
| Content-Type = Application/json  | Header         | -         | Yes      | -                                                            |
| authorization                    | Bearer Token   | -         | Yes      | Token used to check authorization of user to use the API.    |
| appUserId                        | Query String   | -         | Yes      | App User ID                                                  |
| name                             | Body           | string    | Yes      | Card name.                                                   |
| description                      | Body           | string    | No       | Card description.                                            |
| color                            | Body           | string    | No       | Card color.                                                  |


#### Sample Request
{{baseUrl}}/api/appUsers/kev5f943-112f-4d49-888d-c671e210b8b8/cards/

```json
{
  	"Name": "Client Meeting",
	"description": "Discuss project milestone.",
	"color": "#00FF00"
}
```
 
#### Sample Response
```json
{
   	"cardId": "64491916-b423-408c-a29a-6184c24cdf0d",
	"name": "Client Meeting",
	"description": "Discuss project milestone.",
	"dateOfCreation": "2024-03-22T09:10:28.626608Z",
	"status": "ToDo",
	"createdByAppUser": "kev@gmail.com",
	"color": "#00FF00"
}
```
---



 ### Update a Card
|                  |                                                    |
|------------------|----------------------------------------------------|
| HTTP Method      | PUT                                                |
| End-point        | {{baseUrl}}/api/appUsers/{appUserId}/cards/{cardId}|

### Update a Card Parameters
|                                  | Position       | Data Type | Required | Description                                                  |
|----------------------------------|----------------|-----------|----------|--------------------------------------------------------------|
| Content-Type = Application/json  | Header         | -         | Yes      | -                                                            |
| authorization                    | Bearer Token   | -         | Yes      | Token used to check authorization of user to use the API.    |
| appUserId                        | Query String   | -         | Yes      | App User ID                                                  |
| cardId                           | Query String   | String    | Yes      | Card ID.                                                     |
| name                             | Body           | String    | Yes      | Card name.                                                   |
| status                           | Body           | String    | Yes      | Card status.                                                 |
| description                      | Body           | String    | No       | Card description.                                            |
| color                            | Body           | String    | No       | Card color.                                                  |


#### Sample Request
{{baseUrl}}/api/appUsers/kev5f943-112f-4d49-888d-c671e210b8b8/cards/updateDatabase-f8a1-49e2-b7ab-2f5c6d73c93d

```json

    {
       	"name": "Update Database",
		"status": "Done",	
		"description": "Discuss project milestones with the client.",
		"color": "#00FF00"
    }
```

---

 ### Delete a Card
|              |                                                     |
|--------------|-----------------------------------------------------|
| HTTP Method  | DELETE                                              |
| End-point    | {{baseUrl}}/api/appUsers/{appUserId}/cards/{cardId} |

### Delete a Card Parameters
|                                  | Position       | Data Type | Required | Description                                                  |
|----------------------------------|----------------|-----------|----------|--------------------------------------------------------------|
| Content-Type = Application/json  | Header         | -         | Yes      | -                                                            |
| authorization                    | Bearer Token   | -         | Yes      | Token used to check authorization of user to use the API.    |
| appUserId                        | Query String   | -         | Yes      | App User ID                                                  |
| cardId                           | Query String   | String    | Yes      | Card ID.                                                     |

#### Sample Request
{{baseUrl}}/api/appUsers/kev5f943-112f-4d49-888d-c671e210b8b8/cards/clientMeeting-2f9e-4681-a499-4a2d1b2e36e4

---



 ### Get a Single Card
|              |                                                   |
|--------------|---------------------------------------------------|
| HTTP Method  | GET                                               |
| End-point    | {{baseUrl}}/api/appUsers/{appUserId}/cards/{cardId} |

### Get a Single Card Parameters
|                                  | Position       | Data Type | Required | Description                                                  |
|----------------------------------|----------------|-----------|----------|--------------------------------------------------------------|
| Content-Type = Application/json  | Header         | -         | Yes      | -                                                            |
| authorization                    | Bearer Token   | -         | Yes      | Token used to check authorization of user to use the API.    |
| appUserId                        | Query String   | -         | Yes      | App User ID                                                  |
| cardId                           | Query String   | String    | Yes      | Card ID.                                                     |

#### Sample Request
{{baseUrl}}/api/appUsers/kev5f943-112f-4d49-888d-c671e210b8b8/cards/updateDatabase-f8a1-49e2-b7ab-2f5c6d73c93d

#### Sample Response
```json
{
  	"cardId": "updateDatabase-f8a1-49e2-b7ab-2f5c6d73c93d",
	"name": "Update Database",
	"description": "Perform necessary updates on the database.",
	"dateOfCreation": "2024-02-05T15:20:00Z",
	"status": "In Progress",
	"createdByAppUser": "kev@gmail.com",
	"color": "#32CD32"
}
```

---

### Get Cards
|              |                                                     | User Role   |
|--------------|-----------------------------------------------------|-------------|
| HTTP Method  | GET                                                 | -           |
| End-point    | {{baseUrl}}/api/appUsers/{appUserId}/cards/all      | Admin       |
| End-point    | {{baseUrl}}/api/appUsers/{appUserId}/cards/forUser  | Member      |


### Get Cards Parameters
|                                  | Position       | Data Type | Required | Description                                                  |
|----------------------------------|----------------|-----------|----------|--------------------------------------------------------------|
| Content-Type = Application/json  | Header         | -         | Yes      | -                                                            |
| authorization                    | Bearer Token   | -         | Yes      | Token used to check authorization of user to use the API.    |
| appUserId                        | Query String   | -         | Yes      | App User ID                                                  |

#### Sample Request
{{baseUrl}}/api/appUsers/kev5f943-112f-4d49-888d-c671e210b8b8/cards/all

#### Sample Response
```json
[
	{
		"cardId": "clientMeeting-7c8a-4a7d-9533-56a21b5c92e1",
		"name": "Client Meeting",
		"description": "Discuss project milestones and deliverables with the client.",
		"dateOfCreation": "2024-02-25T14:30:00Z",
		"status": "In Progress",
		"createdByAppUser": "sue@gmail.com",
		"color": "#4682B4"
	},
	{
		"cardId": "clientMeeting-6c72-45fe-a7bf-4cd6d1d90c91",
		"name": "Client Meeting",
		"description": "Review project scope and timeline with the client.",
		"dateOfCreation": "2024-03-05T11:00:00Z",
		"status": "ToDo",
		"createdByAppUser": "sue@gmail.com",
		"color": "#8A2BE2"
	},
	{
		"cardId": "a04ce30d-6f36-4fc2-a3b0-e13b9b02b0b8",
		"name": "Security Audit",
		"description": "",
		"dateOfCreation": "2024-04-10T18:41:05.093498Z",
		"status": "ToDo",
		"createdByAppUser": "test@gmail.com",
		"color": "#0400ff"
	},
	{
		"cardId": "4d5a8694-bd75-4199-8531-81f10e90ac1d",
		"name": "Software Upgrade",
		"description": "",
		"dateOfCreation": "2024-04-10T05:40:21.870919Z",
		"status": "ToDo",
		"createdByAppUser": "kev@gmail.com",
		"color": "#00ff00"
	},
	{
		"cardId": "239e7b6b-e0f8-4c9d-a644-74b92e9ad32a",
		"name": "Software Upgrade",
		"description": "",
		"dateOfCreation": "2024-04-10T05:38:49.084651Z",
		"status": "ToDo",
		"createdByAppUser": "kev@gmail.com",
		"color": "#00ff00"
	},
	{
		"cardId": "semIstalltion-8fae-7488fc2c1b95",
		"name": "System Installation",
		"description": "Installation of system to the new client.",
		"dateOfCreation": "2024-01-15T17:37:19Z",
		"status": "ToDo",
		"createdByAppUser": "kev@gmail.com",
		"color": "#FF7F50"
	},
	{
		"cardId": "systemIstallation-8fae-7488fc2c1b95",
		"name": "System Installation",
		"description": "Installation of system to the new client.",
		"dateOfCreation": "2024-01-15T17:37:19Z",
		"status": "ToDo",
		"createdByAppUser": "kev@gmail.com",
		"color": "#FF7F50"
	},
	{
		"cardId": "sysemIstallation-8fae-7488fc2c1b95",
		"name": "System Installation",
		"description": "Installation of system to the new client.",
		"dateOfCreation": "2024-01-15T17:37:19Z",
		"status": "ToDo",
		"createdByAppUser": "kev@gmail.com",
		"color": "#FF7F50"
	},
	{
		"cardId": "semIstallation-8fae-7488fc2c1b95",
		"name": "System Installation",
		"description": "Installation of system to the new client.",
		"dateOfCreation": "2024-01-15T17:37:19Z",
		"status": "ToDo",
		"createdByAppUser": "kev@gmail.com",
		"color": "#FF7F50"
	},
	{
		"cardId": "d10caae9-c6c9-47d4-9b19-bec30b4a94f5",
		"name": "System Intallation",
		"description": null,
		"dateOfCreation": "2024-03-22T16:20:28.600347Z",
		"status": "ToDo",
		"createdByAppUser": "kev@gmail.com",
		"color": null
	}
]
```

---



### Filter Cards
|              |                                                     | User Role   |
|--------------|-----------------------------------------------------|-------------|
| HTTP Method  | GET                                                 | -           |
| End-point    | {{baseUrl}}/api/appUsers/{appUserId}/cards/all      | Admin       |
| End-point    | {{baseUrl}}/api/appUsers/{appUserId}/cards/forUser  | Member      |


### Filter Cards Parameters
|                                  | Position       | Data Type | Required | Description                                                  |
|----------------------------------|----------------|-----------|----------|--------------------------------------------------------------|
| Content-Type = Application/json  | Header         | -         | Yes      | -                                                            |
| authorization                    | Bearer Token   | -         | Yes      | Token used to check authorization of user to use the API.    |
| appUserId                        | Query String   | -         | Yes      | App User ID                                                  |
| name                             | Query String   | -         | No       | Card name                                                    |
| color                            | Query String   | -         | No       | Card color                                                   |
| status                           | Query String   | -         | No       | Card status                                                  |
| dateOfCreation                   | Query String   | -         | No       | Card Date of Creation                                        |


#### Sample Request
{{baseUrl}}/api/appUsers/admin46d-9e9f-44d3-8425-263ba67509aa/cards/all?name=Client%20Meeting&color=%238A2BE2&status=%20todo&dateOfCreation=2024-03-05

#### Sample Response
```json
[
	{
		"cardId": "clientMeeting-6c72-45fe-a7bf-4cd6d1d90c91",
		"name": "Client Meeting",
		"description": "Review project scope and timeline with the client.",
		"dateOfCreation": "2024-03-05T11:00:00Z",
		"status": "ToDo",
		"createdByAppUser": "sue@gmail.com",
		"color": "#8A2BE2"
	}
]
```

---

### Sort Cards
|              |                                                     | User Role   |
|--------------|-----------------------------------------------------|-------------|
| HTTP Method  | GET                                                 | -           |
| End-point    | {{baseUrl}}/api/appUsers/{appUserId}/cards/all      | Admin       |
| End-point    | {{baseUrl}}/api/appUsers/{appUserId}/cards/forUser  | Member      |

### Sort Cards Parameters
|                                  | Position       | Data Type | Required | Description                                                  |
|----------------------------------|----------------|-----------|----------|--------------------------------------------------------------|
| Content-Type = Application/json  | Header         | -         | Yes      | -                                                            |
| authorization                    | Bearer Token   | -         | Yes      | Token used to check authorization of user to use the API.    |
| appUserId                        | Query String   | -         | Yes      | App User ID                                                  |
| orderBy                          | Query String   | -         | Yes      | Order query string parameters                                |

#### Sample Request
{{baseUrl}}/api/appUsers/admin46d-9e9f-44d3-8425-263ba67509aa/cards/all?orderBy=name,color,status%20desc,dateOfCreation%20desc

#### Sample Response
```json
[
	{
		"cardId": "clientMeeting-7c8a-4a7d-9533-56a21b5c92e1",
		"name": "Client Meeting",
		"description": "Discuss project milestones and deliverables with the client.",
		"dateOfCreation": "2024-02-25T14:30:00Z",
		"status": "In Progress",
		"createdByAppUser": "sue@gmail.com",
		"color": "#4682B4"
	},
	{
		"cardId": "clientMeeting-6c72-45fe-a7bf-4cd6d1d90c91",
		"name": "Client Meeting",
		"description": "Review project scope and timeline with the client.",
		"dateOfCreation": "2024-03-05T11:00:00Z",
		"status": "ToDo",
		"createdByAppUser": "sue@gmail.com",
		"color": "#8A2BE2"
	},
	{
		"cardId": "a04ce30d-6f36-4fc2-a3b0-e13b9b02b0b8",
		"name": "Security Audit",
		"description": "",
		"dateOfCreation": "2024-04-10T18:41:05.093498Z",
		"status": "ToDo",
		"createdByAppUser": "test@gmail.com",
		"color": "#0400ff"
	},
	{
		"cardId": "4d5a8694-bd75-4199-8531-81f10e90ac1d",
		"name": "Software Upgrade",
		"description": "",
		"dateOfCreation": "2024-04-10T05:40:21.870919Z",
		"status": "ToDo",
		"createdByAppUser": "kev@gmail.com",
		"color": "#00ff00"
	},
	{
		"cardId": "239e7b6b-e0f8-4c9d-a644-74b92e9ad32a",
		"name": "Software Upgrade",
		"description": "",
		"dateOfCreation": "2024-04-10T05:38:49.084651Z",
		"status": "ToDo",
		"createdByAppUser": "kev@gmail.com",
		"color": "#00ff00"
	},
	{
		"cardId": "semIstalltion-8fae-7488fc2c1b95",
		"name": "System Installation",
		"description": "Installation of system to the new client.",
		"dateOfCreation": "2024-01-15T17:37:19Z",
		"status": "ToDo",
		"createdByAppUser": "kev@gmail.com",
		"color": "#FF7F50"
	},
	{
		"cardId": "sysemIstallation-8fae-7488fc2c1b95",
		"name": "System Installation",
		"description": "Installation of system to the new client.",
		"dateOfCreation": "2024-01-15T17:37:19Z",
		"status": "ToDo",
		"createdByAppUser": "kev@gmail.com",
		"color": "#FF7F50"
	},
	{
		"cardId": "semIstallation-8fae-7488fc2c1b95",
		"name": "System Installation",
		"description": "Installation of system to the new client.",
		"dateOfCreation": "2024-01-15T17:37:19Z",
		"status": "ToDo",
		"createdByAppUser": "kev@gmail.com",
		"color": "#FF7F50"
	},
	{
		"cardId": "systemIstallation-8fae-7488fc2c1b95",
		"name": "System Installation",
		"description": "Installation of system to the new client.",
		"dateOfCreation": "2024-01-15T17:37:19Z",
		"status": "ToDo",
		"createdByAppUser": "kev@gmail.com",
		"color": "#FF7F50"
	},
	{
		"cardId": "d10caae9-c6c9-47d4-9b19-bec30b4a94f5",
		"name": "System Intallation",
		"description": null,
		"dateOfCreation": "2024-03-22T16:20:28.600347Z",
		"status": "ToDo",
		"createdByAppUser": "kev@gmail.com",
		"color": null
	}
]
```

---

### Cards Pagination
|              |                                                     | User Role   |
|--------------|-----------------------------------------------------|-------------|
| HTTP Method  | GET                                                 | -           |
| End-point    | {{baseUrl}}/api/appUsers/{appUserId}/cards/all      | Admin       |
| End-point    | {{baseUrl}}/api/appUsers/{appUserId}/cards/forUser  | Member      |



### Card pagination Parameters
|                                  | Position       | Data Type | Required | Description                                                  |
|----------------------------------|----------------|-----------|----------|--------------------------------------------------------------|
| Content-Type = Application/json  | Header         | -         | Yes      | -                                                            |
| authorization                    | Bearer Token   | -         | Yes      | Token used to check authorization of user to use the API.    |
| appUserId                        | Query String   | -         | Yes      | App User ID                                                  |
| pageNumber                       | Query String   | -         | No       | Which page of results to retrive.                            |
| pageSize                         | Query String   | -         | No       | Number of results to include per page.                       |

#### Sample Request
{{baseUrl}}api/appUsers/admin46d-9e9f-44d3-8425-263ba67509aa/cards/all?pageNumber=2&pageSize=3

#### Sample Response
```json
[
	{
		"cardId": "239e7b6b-e0f8-4c9d-a644-74b92e9ad32a",
		"name": "Software Upgrade",
		"description": "",
		"dateOfCreation": "2024-04-10T05:38:49.084651Z",
		"status": "ToDo",
		"createdByAppUser": "kev@gmail.com",
		"color": "#00ff00"
	},
	{
		"cardId": "4d5a8694-bd75-4199-8531-81f10e90ac1d",
		"name": "Software Upgrade",
		"description": "",
		"dateOfCreation": "2024-04-10T05:40:21.870919Z",
		"status": "ToDo",
		"createdByAppUser": "kev@gmail.com",
		"color": "#00ff00"
	},
	{
		"cardId": "sysemIstallation-8fae-7488fc2c1b95",
		"name": "System Installation",
		"description": "Installation of system to the new client.",
		"dateOfCreation": "2024-01-15T17:37:19Z",
		"status": "ToDo",
		"createdByAppUser": "kev@gmail.com",
		"color": "#FF7F50"
	}
]
```

---

### Get Cards with Pagination, filtering and sorting combined
|              |                                                     | User Role   |
|--------------|-----------------------------------------------------|-------------|
| HTTP Method  | GET                                                 | -           |
| End-point    | {{baseUrl}}/api/appUsers/{appUserId}/cards/all      | Admin       |
| End-point    | {{baseUrl}}/api/appUsers/{appUserId}/cards/forUser  | Member      |


### Get Cards with Pagination, filtering and sorting combined parameters
|                                  | Position       | Data Type | Required | Description                                                  |
|----------------------------------|----------------|-----------|----------|--------------------------------------------------------------|
| Content-Type = Application/json  | Header         | -         | Yes      | -                                                            |
| authorization                    | Bearer Token   | -         | Yes      | Token used to check authorization of user to use the API.    |
| appUserId                        | Query String   | -         | Yes      | App User ID                                                  |
| pageNumber                       | Query String   | -         | No       | Which page of results to retrive.                            |
| pageSize                         | Query String   | -         | No       | Number of results to include per page.                       |
| name                             | Query String   | -         | No       | Card name                                                    |
| color                            | Query String   | -         | No       | Card color                                                   |
| status                           | Query String   | -         | No       | Card status                                                  |
| dateOfCreation                   | Query String   | -         | No       | Card Date of Creation                                        |
| orderBy                          | Query String   | -         | Yes      | Order query string parameters                                |

#### Sample Request
{{baseUrl}}/api/appUsers/admin46d-9e9f-44d3-8425-263ba67509aa/cards/all?pageNumber=1&pageSize=2&name=System%20Installation&color=%23FF7F50&status=%20todo&dateOfCreation=2024-01-15&orderBy=name,color,status%20desc,dateOfCreation%20desc

#### Sample Response
```json
[
	{
		"cardId": "sysemIstallation-8fae-7488fc2c1b95",
		"name": "System Installation",
		"description": "Installation of system to the new client.",
		"dateOfCreation": "2024-01-15T17:37:19Z",
		"status": "ToDo",
		"createdByAppUser": "kev@gmail.com",
		"color": "#FF7F50"
	},
	{
		"cardId": "systemIstallation-8fae-7488fc2c1b95",
		"name": "System Installation",
		"description": "Installation of system to the new client.",
		"dateOfCreation": "2024-01-15T17:37:19Z",
		"status": "ToDo",
		"createdByAppUser": "kev@gmail.com",
		"color": "#FF7F50"
	}
]
```
