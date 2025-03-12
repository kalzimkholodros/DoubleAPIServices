using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Order.API.Services;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

public interface IProductService
{
    Task<ProductDto> GetProductAsync(int id);
}

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<ProductService> _logger;

    public ProductService(HttpClient httpClient, ILogger<ProductService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<ProductDto> GetProductAsync(int id)
    {
        try
        {
            _logger.LogInformation($"Attempting to get product with ID: {id}");
            _logger.LogInformation($"Using base address: {_httpClient.BaseAddress}");
            
            var url = $"api/products/{id}";
            _logger.LogInformation($"Full request URL: {_httpClient.BaseAddress}{url}");
            
            var response = await _httpClient.GetAsync(url);
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Failed to get product. Status code: {response.StatusCode}, Error: {errorContent}");
                throw new HttpRequestException($"Failed to get product. Status code: {response.StatusCode}, Error: {errorContent}");
            }
            
            var content = await response.Content.ReadAsStringAsync();
            _logger.LogInformation($"Received response: {content}");
            
            return JsonSerializer.Deserialize<ProductDto>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Exception while getting product: {ex.Message}");
            throw new HttpRequestException($"Failed to get product information. Error: {ex.Message}", ex);
        }
    }
} 