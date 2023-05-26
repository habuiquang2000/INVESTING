using DataService.Services;
using BaseLib.Dtos.Product;
using BaseLib.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataService.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ProductModel>?> GList(
        string? search
    )
    {
        _logger.LogDebug("GList");
        List<ProductModel>? products = ProductService.ReadMany(search ?? "");

        return products;
    }

    [HttpPost]
    public async Task<ActionResult> PCreate([FromBody] PrductCreateDto product)
    {
        ProductModel newProduct = await ProductService.CreateOne(product);
        return Ok(newProduct);
    }
}