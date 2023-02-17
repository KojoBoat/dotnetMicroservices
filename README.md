# dotnetMicroservices
.NET Core Microservices
Demonstrating microservices by building an e-commerce application. The application will have various microservices handling different tasks.

# Microservices
    - Catalog
        This service will be the Categories section of the e-commerce, a .net core web api project with MongoDB as the database.
    - Basket (Cart)
        This is more of a cart system, a .net core web api project, which uses Redis to implement caching.
    - Discount (gRPC) service is consumed by the Discount Api service. At checkout, system checks to see if there are available discounts for products on the system.
    - Ordering
        This microservice is developed following Clean Architecture principles, CQRS and DDD model. This service handles when a user makes an order from the e-commerce  site, checkout page to be precise, to make payments.

# Technology leveraged
    (/) RabbitMQ
    (/) Ocelot Api Gateway
    (/) Elastic search and kibana
    (/) PostgreSQL
    (/) SQL Server
    (/) MongoDB
    (/) Redis
    (/) Docker

