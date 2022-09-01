namespace Api.Models.OrderModels;

public class OrderModel
{
    public int Id { get; set; }
    public string? Account { get; set; }
    public List<ItemModel>? OrderItems { get; set; }

    public OrderModel(int id, string account, List<ItemModel> orderItems)
    {
        Id = id;
        Account = account;
        OrderItems = orderItems;
    }

    public OrderModel()
    {
    }
}