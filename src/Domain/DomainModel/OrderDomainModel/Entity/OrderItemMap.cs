namespace Domain.DomainModel.OrderDomainModel.Entity;

public class OrderItemMap
{
    public int OrderId { get; set; }
    public int ItemId { get; set; }
    public int Quantity { get; set; }
    public OrderItemMap()
    {
        
    }
}