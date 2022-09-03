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
                    .Find(x => true)
                    .Limit(100)
                    .ToList();
    }

    public async Task<OrderSummaryData> GetByIdAsync(string id)
    {
        var filter = Builders<OrderSummaryData>
                    .Filter
                    .Eq("Id", id);
        return await _context
                        .OrderSummary
                        .Find(filter)
                        .FirstOrDefaultAsync();
    }

    public async Task UpdateOrderSummaryAsync(OrderSummaryData orderSummaryData)
    {
        var filter = Builders<OrderSummaryData>
            .Filter
            .Eq("Id", orderSummaryData.Id);
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
}