namespace Domain.DomainModel.OrderDomainModel.Entity;

public class Order
{
    // [Required]
    // [Key]
    // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Account { get; set; }

    public Order()
    {
        
    }
}