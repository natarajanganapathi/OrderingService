namespace Api.Models.OrderModels;

public class AccountModel
{
    public int Id { get; set; }
    public string? AccountName { get; set; }
    public List<CatalogModel>? OrderItems { get; set; }

    public AccountModel(int id, string accountName, List<CatalogModel> orderItems)
    {
        Id = id;
        AccountName = accountName;
        OrderItems = orderItems;
    }

    public AccountModel()
    {
    }
}