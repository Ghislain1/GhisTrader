# GhisTrader

# Development
## Working with EF
- Add Nuget Packages (Microsoft.EntityFrameworkCore.Design, Microsoft.EntityFrameworkCore)
- Define Entities ==> Represents each table
- Define DbContext ==> Points Tables, Define Relationship between Table
- For *WPF* ==> https://learn.microsoft.com/en-us/ef/core/get-started/wpf
    - Install the Entity Framework NuGet packages i.e (Microsoft.EntityFrameworkCore.Sqlite)
    - Define a Model i.e Blog
    - Define Your own DbContext
- Create 
    - dotnet ef migrations add NewMigration --project ..\GhisTrader.EntityFramework
    - Start project and Target Project 
