using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Order.API.Data;
using Order.API.Models;
using Order.API.Services;

namespace Order.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrderDbContext _context;
    private readonly IProductService _productService;

    public OrdersController(OrderDbContext context, IProductService productService)
    {
        _context = context;
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderEntity>>> GetOrders()
    {
        return await _context.Orders.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderEntity>> GetOrder(int id)
    {
        var order = await _context.Orders.FindAsync(id);

        if (order == null)
        {
            return NotFound();
        }

        return order;
    }

    [HttpPost]
    public async Task<ActionResult<OrderEntity>> CreateOrder(CreateOrderDto createOrderDto)
    {
        try
        {
            var product = await _productService.GetProductAsync(createOrderDto.ProductId);
            
            var order = new OrderEntity
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Quantity = createOrderDto.Quantity,
                UnitPrice = product.Price,
                TotalPrice = product.Price * createOrderDto.Quantity,
                OrderDate = DateTime.UtcNow
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }
        catch (HttpRequestException)
        {
            return BadRequest("Failed to get product information");
        }
    }
}

public class CreateOrderDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
} 