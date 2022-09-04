namespace Domain.DomainModel.OrderDomainModel.Repository;

public interface IOrderRepository
{
    Account Add(Account order);

    void Update(Account order);

    Task<Account?> GetAsync(int orderId);
}