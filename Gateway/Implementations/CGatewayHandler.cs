using BaseLib.Dtos;
using BaseLib.Dtos.Product;
using BaseLib.Models;
using Gateway.BLL;
using Gateway.Interfaces;

namespace Gateway.Implementations;

public class CGatewayHandler : IGatewayHandler
{
    private readonly IConfiguration _configuration;
    private readonly BaseHttpClient _baseHttpClient;
    public CGatewayHandler(IConfiguration configuration)
    {
        _configuration = configuration;
        _baseHttpClient = new(_configuration.GetSection(CConfigAG.HOST_PRODUCT).Value);
    }

    public EResponseResult Api_GetListProduct(Dictionary<string, string?> query)
    {
        try
        {
            List<ProductModel>? productList = _baseHttpClient.Get<List<ProductModel>>(
                _configuration.GetSection(CConfigAG.ENDPOINT_PRODUCT_LIST).Value,
                query
            );

            return new EResponseResult()
            {
                Code = 0,
                Message = "SUCCESS",
                Data = productList
            };
        }
        catch (Exception ex)
        {
            return new EResponseResult()
            {
                Code = ((long)CConfigAG.CODE_ERROR.IMPLEMEN),
                Message = ex.Message,
                Data = null
            };
        }
    }
    public EResponseResult Api_CreateOneProduct(PrductCreateDto TPM)
    {
        try
        {
            ProductModel? productNew = _baseHttpClient.Post<ProductModel>(
                _configuration.GetSection(CConfigAG.ENDPOINT_PRODUCT_CREATE).Value,
                TPM
            );

            return new EResponseResult()
            {
                Code = 0,
                Message = "SUCCESS",
                Data = productNew
            };
        }
        catch (Exception ex)
        {
            // return
            return new EResponseResult()
            {
                Code = (long)CConfigAG.CODE_ERROR.IMPLEMEN,
                Message = ex.Message,
                Data = null
            };
        }
    }
}