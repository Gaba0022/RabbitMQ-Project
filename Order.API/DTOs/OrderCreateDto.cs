using System.ComponentModel.DataAnnotations;

namespace Order.API.DTOs;

public class OrderCreateDto
{
    [Required]
    public string CustomerName { get; set; } = string.Empty;

    [Required]
    public decimal Total { get; set; }
}
