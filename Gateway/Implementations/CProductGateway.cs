using BaseLib.Dtos;
using BaseLib.Dtos.Product;
using BaseLib.Https;
using Gateway.BLL;
using Gateway.Interfaces;

namespace Gateway.Implementations;

public class CProductGateway : IProductHandler
{
    private readonly IConfiguration _configuration;
    private readonly BaseHttpClient _baseHttpClient;
    public CProductGateway(IConfiguration configuration)
    {
        _configuration = configuration;
        _baseHttpClient = new(_configuration.GetSection(CConfigAG.HOST_PRODUCT).Value);
    }

    public async Task<EResponse> Api_GetListProductAsync(Dictionary<string, string?> query)
    {
        try
        {
            ERequest? request = await _baseHttpClient
                .GetAsync<ERequest>(
                _configuration.GetSection(CConfigAG.ENDPOINT_PRODUCT_LIST).Value,
                query
            );

            return new EResponse()
            {
                Code = (long)EResponse.RESPONSE_CODE.SUCCESS,
                Message = EResponse.RESPONSE_CODE.SUCCESS.ToString(),
                Data = request!.Data,
            };
        }
        catch (Exception ex)
        {
            return new EResponse()
            {
                Code = ((long)CConfigAG.CODE_ERROR.IMPLEMEN),
                Message = ex.Message,
                Data = EResponse.RESPONSE_DATA_NULL
            };
        }
    }
    public async Task<EResponse> Api_CreateOneProductAsync(PrductCreateDto TPM)
    {
        try
        {
            ERequest? request = await _baseHttpClient.PostAsync<ERequest>(
                _configuration.GetSection(CConfigAG.ENDPOINT_PRODUCT_CREATE).Value,
                TPM
            );

            return new EResponse()
            {
                Code = (long)EResponse.RESPONSE_CODE.SUCCESS,
                Message = EResponse.RESPONSE_CODE.SUCCESS.ToString(),
                Data = request!.Data
            };
        }
        catch (Exception ex)
        {
            return new EResponse()
            {
                Code = (long)CConfigAG.CODE_ERROR.IMPLEMEN,
                Message = ex.Message,
                Data = EResponse.RESPONSE_DATA_NULL
            };
        }
    }
}