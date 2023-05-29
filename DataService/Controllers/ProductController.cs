using DataService.Services;
using BaseLib.Dtos.Product;
using Microsoft.AspNetCore.Mvc;
using BaseLib.Controllers;
using BaseLib.Dtos;
using Newtonsoft.Json;

namespace DataService.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class ProductController : ApiBaseController
{
    public ProductController() { }

    [HttpGet]
    public async Task<IActionResult> GList(
        string? search
    )
    {
        ERequest responseResult = await ProductService.ReadMany(search ?? "");
        return Content(JsonConvert.SerializeObject(responseResult));
    }

    [HttpPost]
    public async Task<IActionResult> PCreate([FromBody] PrductCreateDto product)
    {
        ERequest responseResult = await ProductService.CreateOne(product);
        return Content(JsonConvert.SerializeObject(responseResult));
    }
}