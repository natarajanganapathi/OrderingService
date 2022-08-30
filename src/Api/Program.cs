var configuration = GetConfiguration();
var Log = CreateLogger().CreateLogger<Startup>();

try
{
    Log.LogInformation("Configuring web host ({ApplicationContext})...", Program.AppName);
    var host = BuildWebHost(configuration, args);

    // Log.Information("Applying migrations ({ApplicationContext})...", Program.AppName);
    // host.MigrateDbContext<OrderDbContext>((context, services) =>
    // {
    //     var env = services.GetService<IWebHostEnvironment>();
    //     var settings = services.GetService<IOptions<OrderingSettings>>();
    //     var logger = services.GetService<ILogger<OrderingContextSeed>>();

    //     new OrderingContextSeed()
    //         .SeedAsync(context, env, settings, logger)
    //         .Wait();
    // })
    // .MigrateDbContext<IntegrationEventLogContext>((_, __) => { });

    Log.LogInformation("Starting web host ({ApplicationContext})...", Program.AppName);
    host.Run();
    return 0;
}
catch (Exception ex)
{
    Log.LogCritical(ex, "Program terminated unexpectedly ({ApplicationContext})!", Program.AppName);
    return 1;
}
// finally
// {
//     Log.CloseAndFlush();
// }

#region Static Methods

IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    var config = builder.Build();

    if (config.GetValue("UseVault", false))
    {
        TokenCredential credential = new ClientSecretCredential(
            config["Vault:TenantId"],
            config["Vault:ClientId"],
            config["Vault:ClientSecret"]);
        builder.AddAzureKeyVault(new Uri($"https://{config["Vault:Name"]}.vault.azure.net/"), credential);
    }

    return builder.Build();
}

IWebHost BuildWebHost(IConfiguration configuration, string[] args)
{
   return WebHost.CreateDefaultBuilder(args)
        .CaptureStartupErrors(false)
        .ConfigureKestrel(options =>
        {
            var port = configuration.GetValue("PORT", 80);
            options.Listen(IPAddress.Any, port, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
            });
            // var ports = GetDefinedPorts(configuration);
            // options.Listen(IPAddress.Any, ports.httpPort, listenOptions =>
            // {
            //     listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
            // });

            // options.Listen(IPAddress.Any, ports.grpcPort, listenOptions =>
            // {
            //     listenOptions.Protocols = HttpProtocols.Http2;
            // });
        })
        .UseStartup<Startup>()
        .ConfigureAppConfiguration(x => x.AddConfiguration(configuration))
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseSerilog()
        .Build();
}

// Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
// {
//     var seqServerUrl = configuration["Serilog:SeqServerUrl"];
//     var logstashUrl = configuration["Serilog:LogstashgUrl"];
//     return new LoggerConfiguration()
//         .MinimumLevel.Verbose()
//         .Enrich.WithProperty("ApplicationContext", Program.AppName)
//         .Enrich.FromLogContext()
//         .WriteTo.Console()
//         .WriteTo.Seq(string.IsNullOrWhiteSpace(seqServerUrl) ? "http://seq" : seqServerUrl)
//         .WriteTo.Http(string.IsNullOrWhiteSpace(logstashUrl) ? "http://logstash:8080" : logstashUrl)
//         .ReadFrom.Configuration(configuration)
//         .CreateLogger();
// }

ILoggerFactory CreateLogger()
{
    return LoggerFactory.Create(builder =>
    {
        builder
        .AddFilter("Default", LogLevel.Information)
        .AddFilter("Api", LogLevel.Information);
    });
}

public partial class Program
{
    public static string? Namespace = typeof(Startup).Namespace;
    public static string? AppName = Namespace?.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
}
#endregion



// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.

// builder.Services.AddControllers();
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// app.UseAuthorization();

// app.MapControllers();

// app.Run();
