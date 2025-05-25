# FinTwin360

FinTwin360 is a comprehensive financial management application designed to help users track their accounts, manage transactions, and gain insights into their financial activities. The application also features an interactive chatbot to assist with queries and provide support.

## Key Features

*   **Account Management:** Create and manage financial accounts.
*   **Transaction Tracking:** Record and view transactions for your accounts.
*   **Categorization:** Organize transactions into predefined or custom categories.
*   **ChatBot:** An interactive assistant to help with common questions and provide support.
*   **Member Management:** Secure user registration and authentication.

## Technology Stack

*   **Backend:** .NET 8, ASP.NET Core Web API
*   **Database:** MySQL (managed via Docker)
*   **ORM:** Entity Framework Core
*   **Authentication:** JWT (JSON Web Tokens)
*   **API Documentation:** Swagger (OpenAPI)
*   **Containerization:** Docker, Docker Compose
*   **Architecture:** Clean Architecture (Domain, Application, Infrastructure, WebAPI)
*   **Key Libraries:**
    *   AutoMapper: For object-to-object mapping.
    *   FluentResults: For handling operation outcomes.
    *   FluentValidation: For request validation.

## Prerequisites

Before you begin, ensure you have the following installed:

*   [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
*   [Docker Desktop](https://www.docker.com/products/docker-desktop/) (or Docker Engine + Docker Compose)
*   [Git](https://git-scm.com/downloads)

## Getting Started

Follow these instructions to get the FinTwin360 application up and running on your local machine.

### 1. Clone the Repository

```bash
git clone <repository-url> # Replace <repository-url> with the actual URL
cd fintwin360
```

### 2. Configure Environment (if needed)

The application is configured to connect to a MySQL database as defined in `docker/docker-compose.yml`. Ensure your `WebAPI/appsettings.Development.json` (or `appsettings.json`) has the correct connection string if you deviate from the provided Docker setup.

The default connection string (targeting the Dockerized MySQL) is typically:
`"Server=localhost;Port=3306;Database=dev;Uid=testuser;Pwd=Secret123!"` (Ensure this matches your infrastructure setup if changed).

### 3. Run the Database

The project uses a MySQL database, which can be easily started using Docker Compose:

```bash
cd docker
docker-compose up -d fintwin360-mysql-db
cd .. 
```
This will start the MySQL database container. The `setup.sql` file in the `docker` directory will initialize the `dev` database.

### 4. Run the WebAPI

Once the database is running, you can start the WebAPI:

```bash
cd WebAPI
dotnet run --launch-profile http 
```
This will launch the API. By default, it should be accessible at:
*   HTTP: `http://localhost:5236`

You should see output in your console indicating that the application has started.

### Alternative: Running with Docker (API)

The `WebAPI` project includes a `Dockerfile` and is configured in `Properties/launchSettings.json` to run on ports `8080` (HTTP) and `8081` (HTTPS) when containerized. 
To run the API in Docker, you would typically:
1. Build the Docker image: `docker build -t fintwin360-api .` (run from the root or specify Dockerfile path)
2. Run the container, ensuring it's on the same Docker network as the database and a port mapping like ` -p 8080:8080`.

For a more integrated Docker setup, you can extend the `docker/docker-compose.yml` file to include the WebAPI service, build its image, and manage it alongside the database.

## API Access and Documentation

Once the WebAPI is running, you can interact with it.

*   **Base URL (local `dotnet run`):** `http://localhost:5236`
*   **Base URL (Docker container, default):** `http://localhost:8080` (if mapped to host port 8080)

### Swagger API Documentation

The API includes Swagger (OpenAPI) documentation, which allows you to explore and test the endpoints interactively.
Once the API is running, you can access the Swagger UI by navigating to:

*   **`/swagger`**

For example, if running locally via `dotnet run`: `http://localhost:5236/swagger`

## Project Structure

This project follows the principles of Clean Architecture to promote separation of concerns, testability, and maintainability.

*   **`src/Domain`**: Contains the core business logic, entities, value objects, and domain events. This layer has no dependencies on other layers.
*   **`src/Application`**: Orchestrates the data flow and use cases. It contains application services, commands, queries, and interfaces for infrastructure concerns. It depends on the Domain layer.
*   **`src/SharedKernel`**: Holds code and abstractions that can be shared across different layers or projects (e.g., base classes, common interfaces).
*   **`Infrastructure`**: Implements the interfaces defined in the Application layer. This includes data persistence (using Entity Framework Core), external service integrations, authentication mechanisms, etc. It depends on the Application and Domain layers.
*   **`WebAPI`**: The entry point of the application. This ASP.NET Core project exposes the API endpoints, handles requests and responses, and depends on the Application and Infrastructure layers.
*   **`docker`**: Contains Docker-related files, including the `docker-compose.yml` for setting up services like the database.
