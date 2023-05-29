using BaseLib.Dtos;
using BaseLib.Dtos.Product;
using Gateway.BLL;
using Gateway.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BaseLib.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
//[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class ProductController : ApiBaseController
{
    private readonly IProductHandler _httpClientHandler;

    public ProductController(
        IProductHandler httpClientHandler
    )
    {
        _httpClientHandler = httpClientHandler;
    }

    [HttpGet]
    //[HttpGet("List/{search=non}")] // Get book by search
    //[HttpGet("List/{id}")] // Get book details
    [Route("List")]
    public async Task<IActionResult> GList(
        string? search
    )
    {
        try
        {
            EResponse responseResult = await _httpClientHandler.Api_GetListProductAsync(new ()
            {
                ["search"] = search ?? "",
            });

            return Content(JsonConvert.SerializeObject(responseResult));
        }
        catch (Exception ex)
        {
            return CreateJsonResponse(new EResponse()
            {
                Code = (long)CConfigAG.CODE_ERROR.GATEWAY,
                Message = ex.Message,
                Data = EResponse.RESPONSE_DATA_NULL
            });
        }
    }

    [HttpPost("Create")]
    public async Task<IActionResult> PCreate([FromBody] PrductCreateDto product)
    {
        try
        {
            EResponse responseResult = await _httpClientHandler.Api_CreateOneProductAsync(product);

            return Content(JsonConvert.SerializeObject(responseResult));
        }
        catch (Exception ex)
        {
            return CreateJsonResponse(new EResponse()
            {
                Code = (long)CConfigAG.CODE_ERROR.GATEWAY,
                Message = ex.Message,
                Data = EResponse.RESPONSE_DATA_NULL
            });
        }
    }
}