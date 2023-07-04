# Minecraft Panel API

The **Minecraft Panel API** is a .NET Core 3.1 Web API designed to interact with the [Minecraft Panel Plugin](https://git.fhict.nl/I436237/minecraft-panel-plugin/).  
Check the Wiki for more information.

## Dependencies

This project is made with [.NET Core 3.1](https://dotnet.microsoft.com/download). Please be sure to have this version of the SDK installed.  
As this is the central point of everything there is no hard dependency on other projects other than this one.  
The only thing that is required to enable the full capabilities of the project is to have all the projects inside it up and running, and that you have entered the correct URL's of the API's in the `appsettings.json` file.

The project uses a [Ocelot](https://github.com/ThreeMammals/Ocelot) gateway to distribute requests to the correct service.  
Authentication/Authorization is handled by [Identity Server](https://github.com/IdentityServer/IdentityServer4).
And it uses [MariaDB](https://mariadb.org/documentation/) as a database storage solution.
The socket connection makes use of [SignalR](https://docs.microsoft.com/en-us/aspnet/core/tutorials/signalr?tabs=visual-studio&view=aspnetcore-3.1)
If you want to run the project inside [Docker](https://www.docker.com/) please ensure it is installed.

## Installation

The `appsettings.json` file contains the links to external API's and the connection string for the database.  
Change these to correclty point to the right services/databases. Ensure the database is a `MariaDB` database.  
To apply the tables to the database use   
```
dotnet ef database update
```  

If you there is an error that `ef` commands are not recognized, please run:  
```
dotnet tool install --global dotnet-ef
``` 
This will install the `ef` [toolset](https://docs.microsoft.com/en-us/ef/core/cli/dotnet).

#### No Docker
To begin using this project please clone the project to a directory of your choosing.  
Run  
```
dotnet restore minecraft-panel-api.sln
```  
to have all the `NuGet` packages installed correctly.  
Subsequent projects can be run with  
```
dotnet run --project minecraft-panel-api.PROJECT_NAME
```  
This will run the chosen project standalone from the others.  

#### With Docker
To run a project with Docker, you can choose one of the `Dockerfiles` and build & run it with:  
```
docker build -f Dockerfile.CHOOSE_PROJECT -t img-auth . && docker run -p 0.0.0.0:5011:5011 --name img img-auth
```  
This will automatically build and run it.

## Usage

When the project is up and running you may make requests to the API by using the url https://IP_ADRESS:PORT/{endpoint}.  
`POST`/`DELETE` etc requests are expected to contain a `JSON` body when sending over data.  
The `SignalR` hub is available at `https://IP_ADRESS:PORT/chathub`.
