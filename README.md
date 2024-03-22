# Cards RESTful web service

## How to Run

### Installation

    clone and open the solution file in Visual Studio

### Configure connection string

Set a database connection string,`DefaultConnection`, in the **Cards.Web** project's appsettings.json or use [Secrets](https://blogs.msdn.microsoft.com/mihansen/2017/09/10/managing-secrets-in-net-core-2-0-apps/)

Example config setting in appsettings.json for a database called `Accounts`:

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

<img src="https://github.com/mikemathu/Point-Of-Sale-System/blob/main/PointOfSaleSystem.Web/wwwroot/AppData/images/ERDiagrams/Cards_Persistence.PNG">

Method 2

- Use pgAdmin to create an empty database named **Cards**. 
- Import the **Cards.sql** file from the **AppData** directory of this project.



### Configure startup in IDE

- Set the Startup Item in your IDE to **https** and start the server.

### Configure an API client for interacting with the API

- Open Insomnia (or another tool) and import the **Cards.json** file from the 'AppData' directory of this project.
