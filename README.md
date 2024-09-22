# 🛥️ Boat and Trip Booking System

## Project Overview
The Boat and Trip Booking System is a comprehensive platform designed to facilitate the booking of boats and trips. The system caters to three types of users: **Admin** 👨‍💼, **Owner** 🛳️, and **Customer** 👥, each with specific functionalities and responsibilities. This application aims to provide a seamless user experience while ensuring secure and efficient operations.

## Objective
The goal of this project is to assess the ability to design and implement a boat and trip booking system using modern .NET technologies while following best practices in software architecture, including design patterns, authentication, logging, and database management.

## Project Requirements
1. **Environment Setup**: Use .NET 8 for developing the application, Entity Framework Core for database management, and implement a SQL Server database for storing data.
2. **Authentication and Authorization**: Implement JWT (JSON Web Token) based authentication 🔑 for three user types: Admin, Owner, and Customer, protecting API endpoints based on user roles to ensure only authorized users can access specific functionalities.
3. **Architecture & Design Patterns**: Implement the CQRS (Command Query Responsibility Segregation) pattern to separate reading and writing operations, enhancing performance and maintainability, alongside using the Repository Pattern to abstract data access and promote testability.
4. **Logging**: Use Serilog 📜 for logging application activities, ensuring that logs include important events such as user actions, errors, and system warnings for better monitoring and debugging.
5. **Error Handling**: Implement a robust error-handling mechanism to manage exceptions and provide meaningful error messages to users and developers.

## User Roles & Responsibilities
### Admin 👨‍💼
- **User Management**: Approve or reject user registrations to ensure only verified users access the platform.
- **Boat Management**: Approve or reject boats registered by owners to maintain quality and safety standards.
- **Reservation Management**: Monitor and manage the list of reservations to oversee all booking activities.

### Owner 🛳️
- **Registration & Approval**: Register on the platform and await approval from the Admin.
- **Boat & Trip Management**: Create and manage trips by selecting boats from their fleet and adding trip details, including pricing and capacity.
- **Offer Additional Services**: Provide customers with options for additional services (e.g., catering, guided tours) to enhance their trip experience.
- **Financial Management**: Manage a wallet 💰 for receiving payments and processing refunds for canceled reservations.

### Customer 👥
- **Registration & Wallet Management**: Register on the platform and manage a wallet for making payments.
- **Trip Booking**: Browse available trips, view detailed descriptions, and book trips with customizations.
- **Boat Booking**: Browse and book available boats, providing any required information for a tailored experience.
- **Manage Booking History**: View past bookings and cancellations to keep track of activities on the platform.

## API Documentation 📖
- Provide detailed API documentation including endpoint descriptions, request/response examples, and error codes. Consider using Swagger or similar tools for automatic API documentation generation.

## Deliverables 📦
1. **Source Code**: Full source code hosted in a Git repository. You can find it [here](https://github.com/ziadhanii/boat-rental-system) with an organized folder structure and meaningful commit messages.
2. **API Documentation**: Comprehensive documentation of API endpoints and their usage.
3. **README File**: This file, providing instructions on running the application and an overview of the architecture.
## Running the Application 🚀

1. Clone the repository:
   ```bash
   git clone https://github.com/ziadhanii/boat-rental-system

2. Ensure you have .NET 8 and SQL Server installed on your machine.

3. Open the NuGet Package Manager Console in Visual Studio.

4. Restore NuGet packages:
   ```bash
   Update-Package -Reinstall
4. Run database migrations:
   ```bash
   Update-Database
5. Start the application:
   ```bash
    dotnet run

## Dependencies

- .NET 8: Framework for application development.
- Entity Framework Core: For database management.
- SQL Server: Database management system.
- Serilog: For structured logging.
- JWT: For authentication and authorization.
## Conclusion

The Boat and Trip Booking System aims to provide a user-friendly platform for booking boats and trips, ensuring a robust architecture and adherence to best practices. This project serves as an excellent opportunity to showcase skills in .NET development, software architecture, and user management. 🚀
