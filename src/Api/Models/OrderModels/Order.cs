namespace Api.Models.OrderModels;

public class OrderModel
{
    public int Id { get; set; }
    public List<ItemModel>? OrderItems { get; set; }

    public OrderModel(int id, List<ItemModel> orderItems)
    {
        Id = id;
        OrderItems = orderItems;
    }

    public OrderModel()
    {
    }
}