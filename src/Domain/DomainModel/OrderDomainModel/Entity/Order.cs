namespace Domain.DomainModel.OrderDomainModel.Entity;

public class Order
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public int CatalogId { get; set; }
    public int Quantity { get; set; }

    public Order()
    {

    }
}