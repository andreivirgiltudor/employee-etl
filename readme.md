# Employee ETL sample
## Setup development box
### Prerequisites
To develop new features on this project you need to have following packages installed:
- [Visual Studio Code](https://code.visualstudio.com/) editor
- [.NET 7.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

### Clone source code
First you need to have [Git](https://git-scm.com/) installed. After installing it, you must run this command to clone this repository on your development box: 
```powershell 
git clone https://github.com/andreivirgiltudor/employee-etl.git
```
From this point forward you're good to go.

## Run the project
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

Now you're good to go. Enjoy!