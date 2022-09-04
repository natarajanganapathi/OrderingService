public class OrderSummaryData
{
    [BsonElement("_id")]
    [BsonId]
    [BsonIgnoreIfDefault]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public int ItemId { get; set; }
    public string? Name { get; set; }
    public int Total { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
