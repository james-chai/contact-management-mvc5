# Contact Management MVC5 (.NET framework 4.8)

## 🛠 Development Setup

### Prerequisites
Ensure the following tools/SDKs are installed:

1. **.NET Framework 4.8**
   [Download from Microsoft](https://dotnet.microsoft.com/download/dotnet-framework/net48)

2. **Visual Studio 2022 (Version 17.9 or later)**
   [Download from Microsoft](https://visualstudio.microsoft.com/downloads/)

   Required workloads:
	- ASP.NET and web development
	- .NET desktop development (for .NET Framework 4.8 support)
   Recommended components:
	- .NET Framework 4.8 targeting pack
	- Git for Windows

3. Connection String Setup
   **NOTE**: Do not use an existing database. The project uses EF Code-First, so you don't need to create a database manually. The database will be created automatically the first time the application is launched. Alternatively, you can create an empty database if you prefer.
 
   In Web.config of the ContactManagement.Web project

    <configuration>
      <configSections>
        <!-- For more information on Entity Framework configuration, visit http://go.microsoft.com/fwlink/?LinkID=237468 -->
        <section name="entityFramework" type="System.Data.Entity.Internal.ConfigFile.EntityFrameworkSection, EntityFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" requirePermission="false" />
      </configSections>
      <connectionStrings>
        <add name="ContactManagementDb" connectionString="Server=localhost;Database=ContactManagement_MVC5;User Id=sa;Password=P@ssw0rd;TrustServerCertificate=True;" providerName="System.Data.SqlClient" />
      </connectionStrings>
      ...
    </configuration>

#### How to run ContactManagement MVC5 application
- Open the ContactManagement_MVC5 solution in Visual Studio 2022
- Set ContactManagement.Web as the startup project
- Then launch it

##### Solution Structure

ContactManagement_MVC5.sln
├── ContactManagement.Web (ASP.NET MVC 5 - .NET 4.8)
│   ├── Controllers
│   │   ├── HomeController.cs
│   │   ├── ContactsController.cs
│   │   └── Error.cs
│   ├── Views
│   │   ├── Contacts
│   │   ├── Home
│   │   ├── Shared
│   │   └── Error
│   ├── App_Start (with AutofacConfig)
│   └── web.config
├── ContactManagement.Core (.NET Standard 2.0)
│   └── Contact.cs
└── ContactManagement.Data (.NET Framework 4.8)
    ├── ContactDbContext.cs
    ├── SqlContactData.cs
    └── IContactData.cs

###### EF6 Database Migrations
NOTE: You don't need to run database migrations manually.
      When you launch the ContactManagement.Web project for the first time,
	  the database will be created automatically using the latest migrations.

1. Set startup project to ContactManagement.Web
2. Open Package Manager Console
3. Default project selected in Package Manager Console dropdown
   → Should be: ContactManagement.Data

4. Run following commands
   # Ensure "Default project" dropdown shows "ContactManagement.Data"
   Enable-Migrations -ContextTypeName ContactManagement.Data.ContactDbContext -MigrationsDirectory Migrations

5. Create and Apply Migrations:
   Add-Migration -Name "InitialCreate"
   Update-Database
