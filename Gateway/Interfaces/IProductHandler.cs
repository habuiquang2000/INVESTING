using BaseLib.Dtos;
using BaseLib.Dtos.Product;

namespace Gateway.Interfaces;

public interface IProductHandler
{
    Task<EResponse> Api_GetListProductAsync(Dictionary<string, string?> query);
    Task<EResponse> Api_CreateOneProductAsync(PrductCreateDto TPM);
}
