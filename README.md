# E-Commerce Microservices

A microservices-based e-commerce application built with .NET Core. It consists of two main services: Product API for managing product catalog and Order API for handling customer orders. The services communicate via HTTP and use PostgreSQL for data persistence.

## Services

### Product API (Port: 5024)
Manages the product catalog including:
- Product creation
- Product listing
- Stock management
- Pricing information

### Order API (Port: 5279)
Handles customer orders:
- Order creation
- Order tracking
- Communicates with Product API to fetch product details
- Calculates total prices

## Technologies
- .NET 8.0
- PostgreSQL
- Entity Framework Core
- Swagger/OpenAPI

## Setup

1. Prerequisites:
   - .NET 8.0 SDK
   - PostgreSQL

2. Database Setup:
   ```bash
   # Create databases
   createdb ProductDb
   createdb OrderDb
   ```

3. Run Migrations:
   ```bash
   # For Product API
   cd Product.API
   dotnet ef database update

   # For Order API
   cd Order.API
   dotnet ef database update
   ```

4. Run the Services:
   ```bash
   # Run Product API (Terminal 1)
   cd Product.API
   dotnet run

   # Run Order API (Terminal 2)
   cd Order.API
   dotnet run
   ```

5. Access Swagger UI:
   - Product API: http://localhost:5024/swagger
   - Order API: http://localhost:5279/swagger

## API Endpoints

### Product API
- `GET /api/products` - Get all products
- `GET /api/products/{id}` - Get product by ID
- `POST /api/products` - Create new product
- `PUT /api/products/{id}` - Update product
- `DELETE /api/products/{id}` - Delete product

### Order API
- `GET /api/orders` - Get all orders
- `GET /api/orders/{id}` - Get order by ID
- `POST /api/orders` - Create new order

## Sample Requests

### Create Product
```json
POST /api/products
{
  "name": "Sample Product",
  "description": "Product description",
  "price": 29.99,
  "stock": 100
}
```

### Create Order
```json
POST /api/orders
{
  "productId": 1,
  "quantity": 2
}
``` 