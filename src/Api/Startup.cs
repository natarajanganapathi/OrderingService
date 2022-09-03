namespace Api.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services
           .AddCors(Configuration)
           .AddDbContext(Configuration)
           // .AddEventBus(Configuration)
           .AddScoped<IDbInitializer, DbInitializer>()
           .AddSingleton<MessageSender>()
           .AddSwagger(Configuration)
           .AddCustomHealthCheck(Configuration)
           .AddMediatR(typeof(Startup).GetTypeInfo().Assembly)
           .AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        var pathBase = Configuration["PATH_BASE"];
        if (!string.IsNullOrEmpty(pathBase))
        {
            loggerFactory.CreateLogger<Startup>().LogDebug("Using PATH BASE '{pathBase}'", pathBase);
            app.UsePathBase(pathBase);
        }
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseSwagger()
        .UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"{(!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty)}/swagger/v1/swagger.json", "Order API - v1");
            // c.OAuthClientId("mobileshoppingaggswaggerui");
            // c.OAuthClientSecret(string.Empty);
            // c.OAuthRealm(string.Empty);
            c.OAuthAppName("Order API Swagger UI");
        });
        app.UseRouting();
        app.UseCors("CorsPolicy");
        app.UseAuthentication();
        // app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
            {
                Predicate = r => r.Name.Contains("self")
            });
        });

        var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
        var scope = scopeFactory.CreateScope();
        var dbInitializer = scope.ServiceProvider.GetService<IDbInitializer>();
        if (dbInitializer != null)
        {
            dbInitializer.Initialize();
            dbInitializer.SeedData();
        }
    }
}
