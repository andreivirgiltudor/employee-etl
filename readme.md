* [Employee ETL API](#employee-etl-api)
* [Notes](#notes)
* [Setup development box](#setup-development-box)
* [Start the project](#start-the-project)
* [Run integration tests](#run-integration-tests)

# Employee ETL API
TODO: INSERT EXERCISE DOC CONTENT
# Notes
## Assumptions:
* Loading data doesn’t occur often.
* Loading must be able to handle large CSV file so the API must offload CSV data extraction, transformation and load on a backend service.
* The HR system has its own database. We use EF Migrations only in the exercise to build the database schema; this shouldn’t happen in the production system.
* All load CSV provides proper header.

## Implementation details:
* The load API service uses Employee HR database to load Employee data.
* The load API service uses Employee ETL database to keep track of its internals like ETL job status.
* The load API offloads ETL processing on a backend service.

## Used libraries
We're listing here only 3rd party libraries that are not common to ASP.NET projects.
* [CSVHelper](https://joshclose.github.io/CsvHelper/) to parse CSV files

## Next things:
* Add authentication and authorization for the load API.
* Add exception middleware.
* Add logging to endpoints.
* Add support Azure BLOB to store and process CSV files (append BLOBs)
* Perform batch inserts (loads)
* Handle failed records processing (log those errors 
* For lager systems, to support massive scale out we can perform the ETL on a different process/service like Azure Functions or ephemeral Docker containers.
    * Introduce queues or web hooks to notify REST API when file has been loaded.
* Provide a different storage mechanism for ETL service internals.
Generalize salary bands support.


# Setup development box
## Prerequisites
To develop new features on this project you need to have following packages installed:
- [Visual Studio Code](https://code.visualstudio.com/) editor
- [.NET 7.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

## Clone source code
First you need to have [Git](https://git-scm.com/) installed. After installing it, you must run this command to clone this repository on your development box: 
```powershell 
git clone https://github.com/andreivirgiltudor/employee-etl.git
```
From this point forward you're good to go.

# Start the project
## Start SQL Server
This product requires a SQL server instance. To avoid installing SQL server instance you can use a Docker container to host one.

To spin-up such a container you have to run this command in the project root folder: 
```powershell 
docker-compose pull
```
This command downloads Microsoft SQL Server docker image. Depending on your internet connection speed it might take a while for the first time. Then you need to configure the SQL Server password. To do this you have to create an .ENV file next to [docker-compose.yml](./docker-compose.yml) with this content:
```ini
ACCEPT_EULA=Y
MSSQL_SA_PASSWORD=[YOUR-SECRET-PASSWORD]
```
**Warning: make sure you add this .ENV file to .gitignore to stop git tracking this file as it contains sensitive information!**

To test your setup, run this command in the project root foldr:
```powershell
docker-compose up
```
This command will start a Docker container with a SQL Server instance. You should have no errors on terminal. To stop this instance you have to CTRL+C on your terminal *(for Windows boxes valid only)*.

You can also start a Docker container with SQL Server instance in detached mode but you will not have the SQL Server trace output available which is very useful to debug and troubleshoot:
```powershell
docker-compose up -d
```

If that's the case, to stop the Docker container you have to issue this command in the project root folder:
```powershell
docker-compose down
```

## Configure connection string
After you've started the database server you need to configure connection strings for load API service and for integration testing services.

You have to navigate to [service root folder](./src/EmployeeETL.WebAPI/) and configure EmployeeETLContext and EmployeeHRContext connection strings:
```powershell
dotnet user-secrets set "ConnectionStrings:EmployeeETLContext" "Server=localhost; Database=EmployeeETL; User Id=sa; Password=[YOUR-SECRET-PASSWORD];TrustServerCertificate=True"

dotnet user-secrets set "ConnectionStrings:EmployeeHRContext" "Server=localhost; Database=EmployeeHR; User Id=sa; Password=[YOUR-SECRET-PASSWORD];TrustServerCertificate=True"
```
* EmployeeETLContext connection string is used to store ETL API internals.
* EmployeeHRContext connection string is used by the HR system ETL API only loads data there.

Of course, you must adjust connection strings to match your environment.

After configuring ETL API you must configure the integration testing project. You have to navigate to [integration testing project](./test/EmployeeETL.WebAPI.IntegrationTests/) and issue the same commands to configure the connection strings.


## Configure databases schema
After connection strings are configured you must create databases schema.
Navigate to [service root folder](./src/EmployeeETL.WebAPI/) and issue this command:
```powershell
dotnet tool install --global dotnet-ef # Install ef tool only if you don't have it
dotnet ef database update --context EmployeeHRContext
dotnet ef database update --context EmployeeETLContext
```

## Run the API
Navigate to [service root folder](./src/EmployeeETL.WebAPI/) and start the API:
```powershell
dotnet run
```

# Run integration tests
To run the integration tests first you need to [start the SQL server](#start-sql-server), [configure connection strings](#configure-connection-string) and [create databases schema](#configure-databases-schema).

Navigate to [integration tests file](./test/EmployeeETL.WebAPI.IntegrationTests/LoadsTests.cs). Running all tests at once fails due to a bad implementation (we did it wrong) or a bug in either [Xunit](https://xunit.net/) or [Microsoft.AspNetCore.Mvc.Testing](https://www.nuget.org/packages/Microsoft.AspNetCore.Mvc.Testing) library so integration tests must be run individually. This is something to investigate.
