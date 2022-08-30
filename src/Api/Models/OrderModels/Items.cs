namespace Api.Models.OrderModels;

public class ItemModel
{
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public int Units { get; set; }

    public ItemModel(int productId, string productName, decimal unitPrice, decimal discount, int units = 1)
    {
        ProductId = productId;
        ProductName = productName;
        UnitPrice = unitPrice;
        Discount = discount;
        Units = units;
    }
    public ItemModel(){}
}