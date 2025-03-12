using Microsoft.EntityFrameworkCore;
using Order.API.Data;
using Order.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Configure HttpClient
builder.Services.AddHttpClient<IProductService, ProductService>(client =>
{
    var productApiUrl = builder.Configuration["ProductApi:Url"];
    client.BaseAddress = new Uri(productApiUrl ?? throw new InvalidOperationException("Product API URL is not configured"));
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use CORS
app.UseCors();

app.UseAuthorization();

app.MapControllers();

// Log the configuration at startup
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation($"Product API URL: {builder.Configuration["ProductApi:Url"]}");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
