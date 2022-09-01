namespace Infrastructure.DatabaseContext;

public class SummaryDataContext
{
    private readonly IMongoDatabase _database;
    public SummaryDataContext(IConfiguration configuration)
    {
        var mongoConnectionString = configuration["MongoConnectionString"];
        MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(mongoConnectionString));
        settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
        var client = new MongoClient(settings);
        if (client == null)
        {
            throw new Exception("Mongo Db not connecting properly");
        }
        var mongoDatabase = configuration["MongoDatabase"];
        _database = client.GetDatabase(mongoDatabase);
    }

    public IMongoCollection<OrderSummaryData> OrderSummary
    {
        get
        {
            return _database.GetCollection<OrderSummaryData>("OrderSummaryData");
        }
    }
}