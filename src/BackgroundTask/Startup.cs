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
            .AddTransient<MessageReceiver>()
            .AddHostedService<InitialLoadMongoDbTask>()
            .AddHostedService<PrepareOrderSummaryTask>()
            // .AddEventBus(this.Configuration);
            ;
    }


    // public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    // {
    //     app.UseRouting();
    //     app.UseEndpoints(endpoints =>
    //     {
    //         endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
    //         {
    //             Predicate = _ => true,
    //             ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    //         });
    //         endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
    //         {
    //             Predicate = r => r.Name.Contains("self")
    //         });
    //     });
    // }
}

