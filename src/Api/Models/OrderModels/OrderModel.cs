namespace Api.Models.OrderModels;

public class OrderModel
{
    public int OrderId { get; set; }
    public int ItemId { get; set; }
    public List<CatalogModel>? Orders { get; set; }
    public List<CatalogModel>? Items { get; set; }

    public OrderModel()
    {
    }
}