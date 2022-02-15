# Interview GamerAPI

An API service that can be used to track and compare lists of users favorite video games.

## Prerequisites

* Visual Studio 2022 ([link](https://visualstudio.microsoft.com/vs/#download) with the ASP.NET and web development workload
* A RAWG Api Key ([link](https://rawg.io/)

## Configuration 

For development and testing set the RAWG api key in the user secrets 

```
dotnet user-secrets set "RAWG:ApiKey" "<API_KEY>"
```

Other settings can be adjusted in appsettings.json

## Run

To run the project open the GamerAPI.sln file in Visual Studio and press F5. The project should open to the about page and display a message.