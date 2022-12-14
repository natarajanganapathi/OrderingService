# Dotnet 6 Sample Project (DDD + CQRS + Materialized View)

1. Program.cs
2. Startup.cs
3. Dependecny Injection
    - Dotnet core DI
4. Logging
    - Serilog
    - SimpleConsole
5. Tracing
    - Azure Application Insights
6. Monitoring
    - Azure Monitor
6. Exception Handling
7. OpenApi Configuration
8. Health Check 
    - Database Health Check
    - Local Resource Manual Health check
    - Azure Service's Health Check
9. Entity Framework
    - Code First
        - Create DB Schema
        - Create Migration Script
        - Execute Migration Script
    - Db First
    - Configuration
    - Validation 
10. DDD - Domain Driven Design
12. CQRS
11. Cors
12. MediatR
13. Configuration
    - appsettings.json
    - Azure Vault Configuration
    - Kubernetes Config Map 
14. CI/CD pipleline
    - Github Actions
15. Filters
    - Global Exception Filter
    - Authentication Filter
16. Container
    - Docker Build
    - Helm Chart
17. Service Bus Integration
    - Azure Service Bus
    - Rabbit MQ
    - Kafka
18. REST Api Maturity Model
19. 


# Reference:

1. [MediatR](https://www.bing.com/videos/search?q=mediatr+c%23+tutorial&view=detail&mid=1C22CB32DCF2534E64D91C22CB32DCF2534E64D9&FORM=VIRE)
2. [Service Bus 1](https://csmithblog.medium.com/azure-service-bus-queue-storage-5780feb17d7c)
3. [Service Bus 2](https://medium.com/nerd-for-tech/azure-service-bus-publish-subscribe-pattern-178dd44baa36)
4. [Service Bus 3 (Topic)](https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-dotnet-how-to-use-topics-subscriptions)
5. [Mongo DB](https://zetcode.com/csharp/mongodb/#:~:text=C%23%20MongoDB%20insert%20document,collection%20with%20the%20InsertOne%20method.&text=The%20example%20inserts%20a%20new,A%20new%20BsonDocument%20is%20created.)
6. [Environmnt Variable](https://www.mailslurp.com/blog/how-to-set-appsettings-config-property-with-environment-variable/)



docker run -it --entrypoint /bin/bash  -p 4200:80 ordering-service/api:linux-latest
docker run -it --network host  -p 4200:80 ordering-service/api:linux-latest    
