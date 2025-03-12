using System.ComponentModel.DataAnnotations;

namespace Order.API.Models;

public class OrderEntity
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public int ProductId { get; set; }
    
    [Required]
    [StringLength(100)]
    public string ProductName { get; set; }
    
    [Required]
    public int Quantity { get; set; }
    
    [Required]
    public decimal UnitPrice { get; set; }
    
    [Required]
    public decimal TotalPrice { get; set; }
    
    [Required]
    public DateTime OrderDate { get; set; }
} 