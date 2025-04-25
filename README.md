# Property CRUD API Structure

# Flow of Operations

 **API: Initiating the Create Operation**
   * The user triggers i.e `CREATE`, `GET`, `UPDATE`, `DELETE` operation via the API.
   * The API performs two actions:
      1. **Adds a New Record in the Property Table**:
         * The property details sent in the request are persisted in the `Property` table in the database.
      2. **Sends a Notification to Azure Service Bus**:
         * A notification message containing property information is sent to the **Azure Service Bus** queue.

**Azure Service Bus: Messaging Queue**
   * Azure Service Bus acts as the messaging backbone between the API and the Azure Function.
   * The notification message remains in the Service Bus queue until it is picked up by a subscriber.

 **Azure Function: Processing the Notification**
   * The Azure Function is triggered automatically when a new message arrives in the Azure Service Bus queue.
   * On trigger, the Azure Function performs the following actions:
      1. **Processes the Notification**:
         * The notification message is read and validated.
      2. **Inserts a Record in the PropertyNotification Table**:
         * A new record is created in the `PropertyNotification` table to log or track the notification details.


# Clean Architecture 

1. Each layer focuses on its designated responsibility, ensuring minimal coupling between layers.
2. High-level modules (e.g., PropertyManagement.Application) depend on abstractions, not concrete implementations.
3. The modular design enables isolated testing of each layer.

# Event-Driven Architecture (EDA)

This system leverages **event-driven principles**, where actions in the system (e.g., a `CREATE` API operation) trigger events that are handled asynchronously by other components. Key elements include:

* **Producers and Consumers**:
   * The **API** acts as an event producer by sending messages to Azure Service Bus.
   * The **Azure Function** acts as a consumer by processing messages from Azure Service Bus.

* **Messaging Queue**:
   * Azure Service Bus decouples the components, enabling asynchronous communication and ensuring reliability.

* **Triggers and Actions**:
   * Events (messages) trigger the Azure Function, which performs follow-up actions, like inserting a record into the `PropertyNotification` table.


## Design Patterns used in this Demo

1. **Repository Pattern**
    Abstracts data access logic from the domain and application layers, making the application database-agnostic.
    Interfaces like IPropertyRepository and their implementations handle CRUD operations on domain entities.
2. **Unit of Work Pattern**
    Ensures that multiple database operations are treated as a single transaction.
    The IUnitOfWork interface and its implementation coordinate repositories to commit or rollback operations.
4. **Dependency Injection**
   Promotes loose coupling by injecting dependencies into components instead of hard-coding them.
   Services and repositories are injected into controllers and other classes using DI containers configured in Program.cs.

## Project Structure

### 1. PropertyManagement.Api
This is the entry point of the application and handles HTTP requests and responses. It includes:
* **Controllers**: Contains API endpoints (e.g., PropertiesController.cs) that interact with services to process requests.
* **Middleware**: Handles cross-cutting concerns like error handling (e.g., ExceptionHandlingMiddleware.cs).
* **Mappings**: Houses mapping profiles for DTOs and domain models.
* **Configuration**: Contains configuration files like `appsettings.json` and `Program.cs` for setting up the application.

### 2. PropertyManagement.Application
This layer defines the application's business logic and contracts:
* **DTOs**: Defines data transfer objects, like `PropertyRequestDto.cs` and `PropertyResponseDto.cs`, to decouple API from domain models.
* **Interfaces**: Declares service contracts, such as `IPropertyService.cs` and `INotificationService.cs`.
* **Services**: Implements application logic in services like `PropertyService.cs` and `NotificationService.cs`.

### 3. PropertyManagement.Domain
This is the core of the application, containing domain models and business rules:
* **Entities**: Core models of the system, like `Address.cs` and `Property.cs`.
* **Interfaces**: Defines domain-level contracts, such as `IPropertyRepository.cs` for data access and `IUnitOfWork.cs` for transaction management.
* **Dependencies**: Houses any common domain-related utilities.

### 4. PropertyManagement.Infrastructure
This layer provides the implementation for data persistence and external integrations:
* **Persistence**: Contains database-related classes, such as EF Core configurations (e.g., `AddressConfiguration.cs`).
* **Repositories**: Implements repository interfaces for data access.
* **Dependencies**: Provides dependencies required for the infrastructure layer.

