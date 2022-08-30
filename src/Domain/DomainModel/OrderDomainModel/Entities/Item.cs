namespace Domain.DomainModel.OrderDomainModel.Entity;

public class Item
{
    // [Required]
    // [Key]
    // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public int Units { get; set; }
    public int Stock { get; set; }
    public Item()
    {
        
    }
}