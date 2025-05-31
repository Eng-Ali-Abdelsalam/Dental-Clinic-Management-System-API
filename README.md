# Dental Clinic Management System API (In Progress)

A comprehensive RESTful API built with ASP.NET Core 9 for managing dental clinic operations, including patient management, appointment scheduling, treatment planning, billing, and document management.

## 🚀 Features

### Core Functionality
- **Patient Management**: Complete patient records with medical history, insurance details, and contact information
- **Appointment Scheduling**: Advanced scheduling system with conflict detection and availability checking
- **Treatment Planning**: Create and manage comprehensive treatment plans with multiple procedures
- **Billing & Invoicing**: Generate invoices, track payments, and manage insurance claims
- **Document Management**: Upload, store, and manage patient documents (X-rays, photos, reports)
- **Dentist Management**: Manage dentist profiles, schedules, and availability

### Advanced Features
- **Automated Notifications**: Appointment reminders and overdue invoice alerts
- **Role-Based Access Control**: Admin, Dentist, Receptionist, and User roles
- **Audit Logging**: Complete audit trail for all operations
- **File Upload System**: Secure document storage with validation
- **Background Jobs**: Scheduled tasks for maintenance and notifications
- **Health Monitoring**: Built-in health checks and monitoring endpoints

## 🛠️ Technology Stack

- **.NET 9** - Latest .NET framework
- **ASP.NET Core Web API** - RESTful API framework
- **Entity Framework Core** - Object-relational mapping
- **SQL Server** - Primary database
- **JWT Authentication** - Secure token-based authentication
- **Hangfire** - Background job processing
- **AutoMapper** - Object-to-object mapping
- **FluentValidation** - Input validation
- **MediatR** - CQRS pattern implementation
- **Serilog** - Structured logging
- **Swagger/OpenAPI** - API documentation
- **xUnit** - Unit testing framework

## 📋 Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB or Express)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)

## 🔧 Installation & Setup

### 1. Clone the Repository
```bash
git clone https://github.com/yourusername/dental-clinic-api.git
cd dental-clinic-api
```

### 2. Restore Dependencies
```bash
dotnet restore
```

### 3. Configure Database Connection
Update the connection strings in `DentalClinic.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=DentalClinicDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True",
    "HangfireConnection": "Server=(localdb)\\mssqllocaldb;Database=DentalClinicHangfire;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
}
```

### 4. Update JWT Settings
Configure JWT settings in `appsettings.json`:

```json
{
  "JwtSettings": {
    "Key": "YourSecretKeyThatShouldBeLongEnoughForProduction",
    "Issuer": "DentalClinicApi",
    "Audience": "DentalClinicClients",
    "DurationInMinutes": 60
  }
}
```

### 5. Create and Seed Database
```bash
cd DentalClinic.Infrastructure
dotnet ef migrations add InitialMigration --startup-project ../DentalClinic.API/DentalClinic.API.csproj
dotnet ef database update --startup-project ../DentalClinic.API/DentalClinic.API.csproj
```

### 6. Run the Application
```bash
cd ../DentalClinic.API
dotnet run
```

The API will be available at:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`
- Swagger UI: `https://localhost:5001/swagger`
- Hangfire Dashboard: `https://localhost:5001/hangfire`

## 🔐 Authentication

### Default Admin Account
- **Email**: `admin@dentalclinic.com`
- **Password**: `Admin@123456`
- **Role**: Admin

### Sample Dentist Accounts
- **Dr. John Smith**: `smith@dentalclinic.com` / `Dentist@123`
- **Dr. Sarah Johnson**: `johnson@dentalclinic.com` / `Dentist@123`

### Getting JWT Token
```bash
POST /api/v1/auth/login
{
  "email": "admin@dentalclinic.com",
  "password": "Admin@123456"
}
```

Use the returned token in the Authorization header:
```
Authorization: Bearer {your-jwt-token}
```

## 📚 API Documentation

The API is fully documented using Swagger/OpenAPI. After running the application, visit:
- **Swagger UI**: `https://localhost:5001/swagger`

### Key API Endpoints

#### Authentication
- `POST /api/v1/auth/login` - User login
- `POST /api/v1/auth/register` - User registration

#### Patients
- `GET /api/v1/patients` - Get all patients
- `GET /api/v1/patients/{id}` - Get patient by ID
- `POST /api/v1/patients` - Create new patient
- `PUT /api/v1/patients/{id}` - Update patient
- `DELETE /api/v1/patients/{id}` - Delete patient

#### Appointments
- `GET /api/v1/appointments` - Get appointments
- `GET /api/v1/appointments/{id}` - Get appointment by ID
- `POST /api/v1/appointments` - Create new appointment
- `POST /api/v1/appointments/available-slots` - Get available time slots

#### Treatments
- `GET /api/v1/treatments` - Get all treatments
- `GET /api/v1/treatments/category/{category}` - Get treatments by category
- `POST /api/v1/treatments` - Create new treatment (Admin/Dentist only)

#### Treatment Plans
- `GET /api/v1/treatmentplans/{id}` - Get treatment plan
- `POST /api/v1/treatmentplans` - Create treatment plan (Dentist only)
- `POST /api/v1/treatmentplans/items` - Add item to treatment plan

#### Documents
- `POST /api/v1/patientdocuments/upload` - Upload patient document
- `GET /api/v1/patientdocuments/{id}/download` - Download document
- `GET /api/v1/patientdocuments/patient/{patientId}` - Get patient documents

## 🏗️ Project Structure

```
DentalClinic.API/                 # Web API Layer
├── Controllers/                  # API Controllers
├── DTOs/                        # Data Transfer Objects
├── Middleware/                  # Custom Middleware
└── Program.cs                   # Application Entry Point

DentalClinic.Core/               # Domain Layer
├── Entities/                    # Domain Entities
├── Enums/                      # Enumerations
├── Interfaces/                 # Domain Interfaces
└── Exceptions/                 # Custom Exceptions

DentalClinic.Application/        # Application Layer
├── Commands/                   # CQRS Commands
├── Queries/                    # CQRS Queries
├── Services/                   # Application Services
├── DTOs/                      # Data Transfer Objects
├── Mapping/                   # AutoMapper Profiles
└── Validators/                # FluentValidation Validators

DentalClinic.Infrastructure/     # Infrastructure Layer
├── Data/                      # Data Access Layer
│   ├── Repositories/          # Repository Implementations
│   ├── Configurations/        # Entity Configurations
│   └── Migrations/           # EF Core Migrations
├── Identity/                  # Authentication & Authorization
└── Services/                 # Infrastructure Services

DentalClinic.Tests/             # Test Projects
├── Unit/                      # Unit Tests
├── Integration/              # Integration Tests
└── API/                     # API Tests
```

## 🧪 Testing

### Run Unit Tests
```bash
dotnet test DentalClinic.Tests/
```

### Test Coverage
```bash
dotnet test --collect:"XPlat Code Coverage"
```

## 🚀 Deployment

### Environment Variables
Set the following environment variables for production:

```bash
ASPNETCORE_ENVIRONMENT=Production
ConnectionStrings__DefaultConnection=YourProductionConnectionString
JwtSettings__Key=YourProductionSecretKey
```

### Docker Support
```dockerfile
# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "DentalClinic.API/DentalClinic.API.csproj"
RUN dotnet build "DentalClinic.API/DentalClinic.API.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "DentalClinic.API/DentalClinic.API.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "DentalClinic.API.dll"]
```

## 📊 Monitoring & Health Checks

### Health Check Endpoint
- **URL**: `/health`
- **Format**: JSON with detailed component status

### Hangfire Dashboard
- **URL**: `/hangfire`
- **Features**: Job monitoring, retry management, performance metrics

### Logging
- **Framework**: Serilog
- **Outputs**: Console, File
- **Structured**: JSON format for production

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Development Guidelines
- Follow Clean Architecture principles
- Write unit tests for new features
- Use conventional commit messages
- Update documentation for API changes
- Ensure code passes all existing tests

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🆘 Support

If you have any questions or need help with setup, please:

1. Check the [Issues](https://github.com/yourusername/dental-clinic-api/issues) page
2. Create a new issue with detailed information
3. Contact the development team

## 🔄 Changelog

### Version 1.0.0
- Initial release with core functionality
- Patient, appointment, and treatment management
- JWT authentication and role-based authorization
- Document upload and management
- Automated background jobs

---

**Built with ❤️ using .NET 9 and ASP.NET Core**
