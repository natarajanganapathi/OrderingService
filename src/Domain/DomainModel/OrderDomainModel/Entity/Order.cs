namespace Domain.DomainModel.OrderDomainModel.Entity;

public class Order
{
    public int OrderId { get; set; }
    public int ItemId { get; set; }
    public int Quantity { get; set; }
    public Order()
    {
        
    }
}