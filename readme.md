
### Install Nuget Package:

```
dotnet add package MySql.EntityFrameworkCore--version 6.0.15
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 6.0.15
dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0.15
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 6.1.1

# packages for authn & authz
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 6.0.16
dotnet add package Microsoft.IdentityModel.Tokens --version 6.29.0
dotnet add package System.IdentityModel.Tokens.Jwt --version 6.29.0
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 8.0.0-preview.3.23177.8e

# logging
dotnet add package Serilog --version 2.10.0
dotnet add package Serilog.AspNetCore --version 6.1.0
dotnet add package Serilog.Sinks.Console --version 4.1.0
```

### Migration Commands

https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli

```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Run Command

```
dotnet build
dotnet run
```

### Swagger

https://localhost:7245/swagger/index.html
