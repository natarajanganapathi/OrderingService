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
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}