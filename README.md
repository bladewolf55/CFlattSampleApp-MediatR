# CFlattSampleApp Web API

## Introduction
This is a sample application with the following features and techniques:

*   Web API
*   Service/Onion/Clean architecture
*   Jimmy Bogard's MediatR pattern[^mediator]
*   MS SQL Server for production and integration testing
*   EF Core
*   Database Migrations in separate project, outputs an *efbundle* for Continuous Deployment
*   Unit tests
*   Integration tests using Visual Studio and ASP.NET test hosting backed by MS SQL Server
*   Local, modular build/test/package script usable by Continuous Integration
*   `appsettings.Override.json`, letting developers use their own settings without committing to Git

[^mediator]: While this solution uses Jimmy Bogard's MediatR package, I'm honestly not yet convinced that it's a better pattern than typical service injection. A big caveat is that I'm not yet using it as he'd recommend (obviously impacting the first concern). This solution fundamentally uses the [Onion Architecture](https://jeffreypalermo.com/2013/08/onion-architecture-part-4-after-four-years/), and Bogard's techniques are tuned to his [Vertical Slice Architecture](https://www.youtube.com/watch?v=SUiWfhAhgQw).

## Libraries
Some of the specialized libraries used are:

*   [MediatR](https://github.com/jbogard/MediatR)
*   [xUnit](https://xunit.net/)
*   [NSubstitute](https://nsubstitute.github.io/)
*   [FluentAssertions](https://fluentassertions.com/)

## Environment
> Changing the environment? Change the CI/CD pipeline!

Install                             | Version    
------------------------------------|------------
.NET                                | 7
PowerShell                          | 7.x
Visual Studio                       | VS 2022
EF Core Tools                       | latest
SQL Server Developer                | 2022
Azure Artifacts Credential Provider | latest (optional)

Sources:
*   [.NET](https://dotnet.microsoft.com/download/dotnet)
*   [PowerShell](https://docs.microsoft.com/en-us/powershell/scripting/install/installing-powershell-core-on-windows)
*   [Visual Studio](https://visualstudio.microsoft.com/downloads/)
*   [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
*   [Azure Artifacts Credential Provider](https://github.com/microsoft/artifacts-credprovider#azure-artifacts-credential-provider)

## Getting Started
> Be sure to use the correct versions from above.

**Environment**  
1.  Install PowerShell 7.x.
1.  Install .NET 7.
1.  Install SQL Server as default instance
1.  Install Azure Artifacts Credential Provider (if using)
    ```powershell
    # Windows PowerShell
    iex "& { $(irm https://aka.ms/install-artifacts-credprovider.ps1) }"
    
    # Mac/Linux
    wget -qO- https://aka.ms/install-artifacts-credprovider.sh | bash
    ```

```powershell
$ErrorActionPreference = 'Stop'
# ef core tools are installed as part of the build script
```

**Application** 
```powershell
# Clone source
$ErrorActionPreference = 'Stop'
cd "c:/src"
git clone https://BHE-IT-01@dev.azure.com/BHE-IT-01/BHE-Technology/_git/operations.pac.psp
cd operations.pac.psp
# One-time-only interactive restore, to get prompted for Azure Artifacts credentials if necessary
dotnet restore --interactive dotnet/webapi
# Build app
./dotnet/webapi/build/build.ps1
# Run SQL migrations
cd package/Migrations/CFlattSampleApp.Migrations
./efbundle.exe
# Run IntegrationTests (Optional)
cd ../../../
dotnet test dotnet\WebApi\CFlattSampleApp.IntegrationTests
```

## Pipeline
The pipeline should run the build script with the -CI option.

```powershell
./dotnet/webapi/build/build.ps1 -CI
```

## Daily Development
Things needed to smoothly develop, such as how to run service dependencies.

## Migrations
Example:

```powershell
dotnet ef migrations add InitialCreate --project CFlattSampleApp.Migrations --startup-project CFlattSampleApp.Migrations --context CFlattSampleAppDbContext
```

## When to run the build script
```powershell
./build/build.ps1
```
The build script simulates what the CI server will do, and is intended to catch build errors before they get to the server. The script should be run before the "final" PR commit. That is:

1.  Before pushing the final feature branch, AND/OR
1.  Before pushing to the mainline branch

## Troubleshooting
