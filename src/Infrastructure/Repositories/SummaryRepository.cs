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

    public async Task<OrderSummaryData> GetByIdAsync(string itemId)
    {
        var filter = Builders<OrderSummaryData>
                    .Filter
                    .Eq("ItemId", itemId);
        return await _context
                        .OrderSummary
                        .Find(filter)
                        .FirstOrDefaultAsync();
    }

    public async Task UpdateOrderSummaryAsync(OrderSummaryData orderSummaryData)
    {
        var filter = Builders<OrderSummaryData>
            .Filter
            .Eq("ItemId", orderSummaryData.ItemId);
        var update = Builders<OrderSummaryData>
            .Update
            .Set("ItemId", orderSummaryData.ItemId)
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
        await _context.OrderSummary.InsertOneAsync(data);
    }

    public async Task CreateManyAsync(List<OrderSummaryData> data)
    {
        await _context.OrderSummary.InsertManyAsync(data);
    }

    public async Task DeleteAsync(int itemId)
    {
        var filter = Builders<OrderSummaryData>
            .Filter
            .Eq("ItemId", itemId);
        await _context.OrderSummary.DeleteOneAsync(filter);
    }
}