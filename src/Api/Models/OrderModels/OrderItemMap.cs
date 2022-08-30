namespace Api.Models.OrderModels;

public class OrderItemMapModel
{
    public int OrderId { get; set; }
    public int ItemId { get; set; }
    public List<ItemModel>? Orders { get; set; }
    public List<ItemModel>? Items { get; set; }

    public OrderItemMapModel()
    {
    }
}