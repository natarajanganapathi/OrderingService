namespace BackgroundTasks;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public virtual void ConfigureServices(IServiceCollection services)
    {
        services
            // .AddCustomHealthCheck(this.Configuration)
            // .Configure<BackgroundTaskSettings>(this.Configuration)
            .AddOptions()
            .AddSingleton<MessageReceiver>()
            .AddSingleton<SummaryDataContext>()
            .AddSingleton<SummaryRepository>()
            .AddHostedService<PrepareSummaryDataTask>()
            .AddDbContext<OrderDbContext>(options =>
                    {
                        options.UseSqlServer(Configuration["ConnectionString"],
                                sqlServerOptionsAction: sqlOptions =>
                                {
                                    sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                    //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                });
                    });
            // .AddHostedService<AddSummaryDataTask>()
            // .AddHostedService<UpdateSummaryDataTask>()
            // .AddEventBus(this.Configuration);
            ;
    }


    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.UseRouting();
        // app.UseEndpoints(endpoints =>
        // {
        //     endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
        //     {
        //         Predicate = _ => true,
        //         ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        //     });
        //     endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
        //     {
        //         Predicate = r => r.Name.Contains("self")
        //     });
        // });
    }
}

