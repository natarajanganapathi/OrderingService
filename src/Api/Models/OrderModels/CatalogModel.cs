namespace Api.Models.OrderModels;

public class CatalogModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public int Units { get; set; }

    public CatalogModel(int id, string name, decimal unitPrice, decimal discount, int units = 1)
    {
        Id = id;
        Name = name;
        UnitPrice = unitPrice;
        Discount = discount;
        Units = units;
    }
    public CatalogModel(){}
}