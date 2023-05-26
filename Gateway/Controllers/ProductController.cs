using BaseLib.Dtos;
using BaseLib.Dtos.Product;
using Gateway.BLL;
using Gateway.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BaseLib.Controllers;

//[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
//[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class ProductController : ApiBaseController
{
    private readonly ILogger<ProductController> _logger;
    private readonly IGatewayHandler _httpClientHandler;

    public ProductController(
        ILogger<ProductController> logger,
        IGatewayHandler handler
    )
    {
        _logger = logger;
        _httpClientHandler = handler;
    }

    [HttpGet]
    //[HttpGet("List/{search=non}")]
    [Route("List")]
    public IActionResult GList(
        string? search
    )
    {
        _logger.LogInformation("Get Product List");

        try
        {
            EResponseResult responseResult = _httpClientHandler.Api_GetListProduct(new ()
            {
                ["search"] = search ?? "",
            });

            return Content(JsonConvert.SerializeObject(responseResult));
        }
        catch (Exception ex)
        {
            return CreateJsonResponse(new EResponseResult()
            {
                Code = (long)CConfigAG.CODE_ERROR.GATEWAY,
                Message = ex.Message,
                Data = null
            });
        }
    }

    [HttpPost("Create")]
    public IActionResult PCreate([FromBody] PrductCreateDto product)
    {
        try
        {
            EResponseResult responseResult = _httpClientHandler.Api_CreateOneProduct(product);

            return Content(JsonConvert.SerializeObject(responseResult));
        }
        catch (Exception ex)
        {
            //return null
            return CreateJsonResponse(new EResponseResult()
            {
                Code = (long)CConfigAG.CODE_ERROR.GATEWAY,
                Message = ex.Message,
                Data = null
            });
        }
    }
}