namespace Infrastructure.Repositories;

public class SummaryRepository
{
    private readonly SummaryDataContext _context;

    public SummaryRepository(SummaryDataContext context)
    {
        _context = context;
    }

    public List<OrderSummaryData> Get()
    {
        return _context
                    .OrderSummary
                    .Find(new BsonDocument())
                    .Limit(100)
                    .ToList();
    }

    public async Task<OrderSummaryData> GetByIdAsync(string catalogId)
    {
        var filter = Builders<OrderSummaryData>
                    .Filter
                    .Eq("catalogId", catalogId);
        return await _context
                        .OrderSummary
                        .Find(filter)
                        .FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(OrderSummaryData orderSummaryData)
    {
        var filter = Builders<OrderSummaryData>
            .Filter
            .Eq("CatalogId", orderSummaryData.CatalogId);
        var update = Builders<OrderSummaryData>
            .Update
            .Set("CatalogId", orderSummaryData.CatalogId)
            .Set("Name", orderSummaryData.Name)
            .Set("Total", orderSummaryData.Total)
            .CurrentDate("UpdateDate");

        await _context
            .OrderSummary
            .UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });
    }
    public async Task CreateAsync(OrderSummaryData data)
    {
        // var doc = new BsonDocument
        //     {
        //         {nameof(OrderSummaryData.Name), data.Name},
        //          {nameof(OrderSummaryData.Name), data.Name},
        //           {nameof(OrderSummaryData.Name), data.Name},
        //            {nameof(OrderSummaryData.Name), data.Name}
        //     };
        data.CreatedDate = DateTime.Now;
        data.UpdatedDate = DateTime.Now;
        await _context.OrderSummary.InsertOneAsync(data);
    }

    public async Task CreateManyAsync(List<OrderSummaryData> data)
    {
        await _context.OrderSummary.InsertManyAsync(data);
    }

    public async Task DeleteAsync(int catalogId)
    {
        var filter = Builders<OrderSummaryData>
            .Filter
            .Eq("CatalogId", catalogId);
        await _context.OrderSummary.DeleteOneAsync(filter);
    }
}