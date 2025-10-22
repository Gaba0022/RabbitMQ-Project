using System.ComponentModel.DataAnnotations;

namespace Order.API.Entities;

public class CustomerOrder
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string CustomerName { get; set; } = string.Empty;

    [Required]
    public decimal Total { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
