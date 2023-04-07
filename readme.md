
### Install Nuget Package:

```
dotnet add package MySql.EntityFrameworkCore--version 6.0.15
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 6.0.15
dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.0.15
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
