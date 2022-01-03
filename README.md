
# Learning Management System

A system that monitors the project between the Academician and the Student.

## Technical Features

- Angular 12 & .NET Core 3.1 WEB API
- RESTful
- Entity Framework Core
- Identity Framework & JWT
- Automapper
- N-Layer Architecture
- Repository Design Pattern
- Dependency Injection


  
## Dependencies

This project uses devextreme packages front and backend side.

  If you do not have previous experience with devexpress, it may be useful to take a look at the links below.

  [Devexpress Angular Documentation](https://js.devexpress.com/Documentation/Guide/Angular_Components/Getting_Started/Add_DevExtreme_to_an_Angular_CLI_Application/)
  
  [Devexpress ASP.NET Core Sample Project](https://github.com/DevExpress/DevExtreme.AspNet.Data/tree/master/net/Sample)


## Overview

MEU LMS; It is a system that monitors homework and projects between the academician and the student, allows grading if the homework/project is submitted between the given dates, closes the grading system if it is not delivered, and shares announcements and exam results.


#### SQL Server Integration

You should replace the OnConfiguring method in LmsDbContext.cs with your own database information by following the path meu.lms > meu.lms.dataaccess > EntityFrameworkCore > Contexts.

```c#
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.UseSqlServer("Server = YOUR_SERVER_PATH; Database = SPECIFIED_DB_NAME; User Id = USER_ID; Password = USER_PASS;");
    base.OnConfiguring(optionsBuilder);
}
```

  
## Using

Install the database on the .NET Core side

```bash
  add-migration Initial
  update-database
```

Run the server

```bash
  dotnet run
```

Install missing packages on the Angular side

```bash
  npm install
```

Run the client

```bash
  ng-serve
```

  