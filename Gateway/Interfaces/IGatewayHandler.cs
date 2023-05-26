using BaseLib.Dtos;
using BaseLib.Dtos.Product;

namespace Gateway.Interfaces;

public interface IGatewayHandler
{
    EResponseResult Api_GetListProduct(Dictionary<string, string?> query);
    EResponseResult Api_CreateOneProduct(PrductCreateDto TOM);
}
