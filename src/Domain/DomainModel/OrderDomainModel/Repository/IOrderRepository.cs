namespace Domain.DomainModel.OrderDomainModel.Repository;

public interface IOrderRepository
{
    Order Add(Order order);

    void Update(Order order);

    Task<Order?> GetAsync(int orderId);
}