using System.ComponentModel.DataAnnotations;

namespace Product.API.Models;

public class ProductEntity
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    
    [StringLength(500)]
    public string Description { get; set; }
    
    [Required]
    public decimal Price { get; set; }
    
    [Required]
    public int Stock { get; set; }
} 