<img alt="Static Badge" src="https://img.shields.io/badge/C%23-%23512BD4?style=plastic&logo=dotnet"> <img alt="Static Badge" src="https://img.shields.io/badge/ASP.NET_Core_MVC-%23512BD4?style=plastic"> <img alt="Static Badge" src="https://img.shields.io/badge/EF_Core-%23512BD4?style=plastic"> <img alt="Static Badge" src="https://img.shields.io/badge/SQL_Server-%23ef901c?style=plastic">
<img alt="GitHub License" src="https://img.shields.io/github/license/mashape/apistatus?style=plastic">

# 🔗 URL Shortener Application

## 📃 Table of Contents
- [Description](#-description)
- [Prerequisites](#-prerequisites)
- [Installation](#-installation)
- [Features](#-features)
- [License](#-license)

## 📖 Description
### Overview
This is a web application that generates short versions of HTTP links with tracking and analytics functionality. It allows users to input long HTTP URLs and receive two shortened URLs in return:

- A public short URL that redirects to the original URL
- A secret stats URL that displays analytics for that short link

A length restriction and format validation ensure that the provided URL is valid. The public short URL tracks and stores each URL access, including the user's IP address. Statistics are recorded asynchronously to ensure a fast and responsive user experience. The generated secret stats URL is secure and difficult to guess.

### Statistics Provided
- Unique visits per day (each IP is counted once per day)
- Top 10 visitor IPs by total opens
- Chart visualization

### Technologies Used
- .NET 8 / ASP.NET Core MVC
- Entity Framework Core with SQL Server (relational database)
- Dependency Injection to implement service architecture
- Background service (UrlAccessLoggingService) for asynchronous logging 
- [jsDelivr](https://www.jsdelivr.com/) - a free CDN used to deliver Chart.js for visualizations
- Bootstrap for UI styling

### Tools
- Postman for testing client IP address retrieval via the "X-Forwarded-For" header in GET requests

### Project Structure
📁 Directories:
- BackgroundServices - Contains a DTO for temporarily storing URL access information, a queue for handling log DTOs, and a background service for asynchronous logging
- Constants - Contains application-wide constants
- Controllers - Contains MVC controllers
- Data - Contains data models, the DB context, utility classes,  entity configurations, and seed data
- Extensions - Contains extension methods
- Helpers - Contains helper classes for URL normalization and IP address retrieval
- Migrations - Contains EF Core migrations for DB creation and seeding
- Models - Contains view models
- Services - Contains business logic and service interfaces
- Validation - Contains a custom URL validation attribute and a custom route constraint for short URL validation
- Views - Contains Razor views for URL input, error display, and statistics

## 📦 Prerequisites
- An IDE (e.g., Visual Studio 2022+ or Visual Studio Code)
- .NET 8 SDK
- MS SQL Server with a client tool (e.g., SQL Server Management Studio or the MSSQL extension for Visual Studio Code)

## 🔧 Installation
1. Clone the repository: [URLShortenerApplication](https://github.com/karinanikolova/URLShortenerApplication)
2. After opening the solution, configure your local connection string.  

If you wish to run the app in development mode, you can either:
- Go to the *ServiceCollectionExtension.cs* file (path: *URLShortenerApp\Extensions\ServiceCollectionExtension.cs*). Find the following code in lines 21 and 22 and place the connection string instead of *configuration.GetConnectionString("DefaultConnection")*:

```
services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
```

- Open the *secrets.json* file by right-clicking on the *URLShortenerApplication* project file, then selecting *Manage User Secrets*, and place your connection string there:

```
{
    "ConnectionStrings": {
        "DefaultConnection": "your_connection_string"
    }
}
```  
If you wish to run the application in production mode, you should hardcode or use a production-safe configuration method in *ServiceCollectionExtension.cs*.

3. Apply the migrations for database creation and data seeding:
- If working with PMC, use *Update-Database* command
- If working with .NET CLI, use *dotnet ef database update* command

4. Run the application.

## 💡 Features
- URL input, normalization, and format validation
- Generation of short URLs and secret statistics URLs
- IP address logging using X-Forwarded-For when available
- Unique visit calculation per IP per day
- Display of top 10 IP addresses by access count
- Visual statistics using Chart.js
- Asynchronous visit logging via BackgroundService (UrlAccessLoggingService)
- Clean and extendable architecture


## 🔓 License
This project is licensed under the MIT License - see the [LICENSE](https://github.com/karinanikolova/URLShortenerApplication/blob/master/LICENSE.txt) file for details.
