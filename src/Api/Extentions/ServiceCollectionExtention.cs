namespace Api.Ententinos;

public static class ServiceCollectionExtention
{
    // public static IServiceCollection AddAppInsight(this IServiceCollection services, IConfiguration configuration)
    // {
    //     services.AddApplicationInsightsTelemetry(configuration);
    //     services.AddApplicationInsightsKubernetesEnricher();

    //     return services;
    // }

    public static IServiceCollection AddFillters(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            });
        return services;
    }
    public static IServiceCollection AddCors(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder
                .SetIsOriginAllowed((host) => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
        });
        return services;
    }

    public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
    {
        var accountName = configuration.GetValue<string>("AzureStorageAccountName");
        var accountKey = configuration.GetValue<string>("AzureStorageAccountKey");

        var hcBuilder = services.AddHealthChecks();

        hcBuilder
            .AddCheck("self", () => HealthCheckResult.Healthy())
            .AddSqlServer(
                configuration["ConnectionString"],
                name: "OrderDB-check",
                tags: new string[] { "OrderDb" });

        if (!string.IsNullOrEmpty(accountName) && !string.IsNullOrEmpty(accountKey))
        {
            hcBuilder
                .AddAzureBlobStorage(
                    $"DefaultEndpointsProtocol=https;AccountName={accountName};AccountKey={accountKey};EndpointSuffix=core.windows.net",
                    name: "catalog-storage-check",
                    tags: new string[] { "catalogstorage" });
        }

        if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
        {
            hcBuilder
                .AddAzureServiceBusTopic(
                    configuration["EventBusConnection"],
                    topicName: "order_event_bus",
                    name: "order-servicebus-check",
                    tags: new string[] { "servicebus" });
        }
        else
        {
            hcBuilder
                .AddRabbitMQ(
                    $"amqp://{configuration["EventBusConnection"]}",
                    name: "order-rabbitmqbus-check",
                    tags: new string[] { "rabbitmqbus" });
        }

        return services;
    }

    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddEntityFrameworkSqlServer()
            .AddDbContext<OrderDbContext>(options =>
        {
            options.UseSqlServer(configuration["ConnectionString"],
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                        //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
        });
        return services;
    }

    // public static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
    // {
    //     services.Configure<CatalogSettings>(configuration);
    //     services.Configure<ApiBehaviorOptions>(options =>
    //     {
    //         options.InvalidModelStateResponseFactory = context =>
    //         {
    //             var problemDetails = new ValidationProblemDetails(context.ModelState)
    //             {
    //                 Instance = context.HttpContext.Request.Path,
    //                 Status = StatusCodes.Status400BadRequest,
    //                 Detail = "Please refer to the errors property for additional details."
    //             };

    //             return new BadRequestObjectResult(problemDetails)
    //             {
    //                 ContentTypes = { "application/problem+json", "application/problem+xml" }
    //             };
    //         };
    //     });

    //     return services;
    // }

    public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {
            // options.DescribeAllEnumsAsStrings();
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Order HTTP API",
                Version = "v1",
                Description = "The Order Microservice HTTP API. This is a Data-Driven/CRUD microservice sample"
            });
        });

        return services;

    }

    // public static IServiceCollection AddIntegrationServices(this IServiceCollection services, IConfiguration configuration)
    // {
    //     services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
    //         sp => (DbConnection c) => new IntegrationEventLogService(c));

    //     services.AddTransient<ICatalogIntegrationEventService, CatalogIntegrationEventService>();

    //     if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
    //     {
    //         services.AddSingleton<IServiceBusPersisterConnection>(sp =>
    //         {
    //             var settings = sp.GetRequiredService<IOptions<CatalogSettings>>().Value;
    //             var logger = sp.GetRequiredService<ILogger<DefaultServiceBusPersisterConnection>>();

    //             var serviceBusConnection = new ServiceBusConnectionStringBuilder(settings.EventBusConnection);

    //             return new DefaultServiceBusPersisterConnection(serviceBusConnection, logger);
    //         });
    //     }
    //     else
    //     {
    //         services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
    //         {
    //             var settings = sp.GetRequiredService<IOptions<CatalogSettings>>().Value;
    //             var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

    //             var factory = new ConnectionFactory()
    //             {
    //                 HostName = configuration["EventBusConnection"],
    //                 DispatchConsumersAsync = true
    //             };

    //             if (!string.IsNullOrEmpty(configuration["EventBusUserName"]))
    //             {
    //                 factory.UserName = configuration["EventBusUserName"];
    //             }

    //             if (!string.IsNullOrEmpty(configuration["EventBusPassword"]))
    //             {
    //                 factory.Password = configuration["EventBusPassword"];
    //             }

    //             var retryCount = 5;
    //             if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
    //             {
    //                 retryCount = int.Parse(configuration["EventBusRetryCount"]);
    //             }

    //             return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
    //         });
    //     }

    //     return services;
    // }

    // public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    // {
    //     var subscriptionClientName = configuration["SubscriptionClientName"];

    //     if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
    //     {
    //         services.AddSingleton<IEventBus, EventBusServiceBus>(sp =>
    //         {
    //             var serviceBusPersisterConnection = sp.GetRequiredService<IServiceBusPersisterConnection>();
    //             var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
    //             var logger = sp.GetRequiredService<ILogger<EventBusServiceBus>>();
    //             var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

    //             return new EventBusServiceBus(serviceBusPersisterConnection, logger,
    //                 eventBusSubcriptionsManager, subscriptionClientName, iLifetimeScope);
    //         });

    //     }
    //     else
    //     {
    //         services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
    //         {
    //             var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
    //             var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
    //             var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
    //             var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

    //             var retryCount = 5;
    //             if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
    //             {
    //                 retryCount = int.Parse(configuration["EventBusRetryCount"]);
    //             }

    //             return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
    //         });
    //     }

    //     services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
    //     services.AddTransient<OrderStatusChangedToAwaitingValidationIntegrationEventHandler>();
    //     services.AddTransient<OrderStatusChangedToPaidIntegrationEventHandler>();

    //     return services;
    // }

    // public static IServiceCollection ConfigureAuthService(IServiceCollection services, IConfiguration configuration)
    // {
    //     // prevent from mapping "sub" claim to nameidentifier.
    //     bool v = JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
    //     var identityUrl = configuration.GetValue<string>("IdentityUrl");
    //     services.AddAuthentication(options =>
    //     {
    //         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    //     }).AddJwtBearer(options =>
    //     {
    //         options.Authority = identityUrl;
    //         options.RequireHttpsMetadata = false;
    //         options.Audience = "basket";
    //     });
    //     return services;
    // }
}