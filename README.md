# Blood Bank Management REST API

This project is a RESTful API for managing a blood bank system, built with C# and ASP.NET Core.

## Features
- CRUD Operations
- Pagination
- Search functionality
- Swagger API documentation

## Endpoints
- `POST /api/bloodbank`: Create a new blood bank entry.
- `GET /api/bloodbank`: Get all entries with optional pagination.
- `GET /api/bloodbank/{id}`: Retrieve entry by ID.
- `PUT /api/bloodbank/{id}`: Update an entry.
- `DELETE /api/bloodbank/{id}`: Delete an entry.
- `GET /api/bloodbank/search`: Search by blood type, status, or donor name.

## How to Run
1. Clone the repository.
2. Build and run the project in Visual Studio.
3. Navigate to `https://localhost:<port>/swagger` for API documentation.

## Testing
- Use Swagger or Postman to test the endpoints.
- Example JSON payload for creating an entry:
```json
{
  "donorName": "John Doe",
  "age": 30,
  "bloodType": "A+",
  "contactInfo": "john.doe@example.com",
  "quantity": 500,
  "collectionDate": "2024-11-18T00:00:00",
  "expirationDate": "2025-01-18T00:00:00",
  "status": "Available"
}
```
## Screenshots
A folder named screenshots is attached to this repository.
It contains:
Swagger UI examples for various endpoints and their responses.
Postman request and response samples for different scenarios, including successful and unsuccessful cases.
