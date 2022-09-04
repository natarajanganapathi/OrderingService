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
            .AddScoped<SummaryDataContext>()
            .AddScoped<SummaryRepository>()
            .AddHostedService<PrepareSummaryDataTask>()
            .AddHostedService<AddSummaryDataTask>()
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

