# dotnetMicroservices
.NET Core Microservices
Demonstrating microservices by building an e-commerce application. The application will have various microservices handling different tasks.

# Microservices
    - Catalog API
        This service will be the Categories section of the e-commerce, a .net core web api project which depends on the MongoDB.
    - Basket API
        This is more of a cart system, a .net core web api project, which uses Redis to implement caching.
    - Discount (gRPC) service is consumed by the Discount Api service. At checkout, system checks to see if there are available discounts for products on the system.
    - Ordering API

# Technology leveraged
    (/) RabbitMQ
    (/) Ocelot Api Gateway
    (/) Elastic search and kibana
    (/) PostgreSQL
    (/) SQL Server
    (/) MongoDB
    (/) Redis
    (/) Docker

