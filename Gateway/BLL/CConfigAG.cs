namespace Gateway.BLL;

public class CConfigAG
{
    public static readonly string HOST_PRODUCT = "DataServiceHosts:DSProductApiService";
    public const string ENDPOINT_PRODUCT_LIST = "ENDPOINT:ApiProductList";
    public const string ENDPOINT_PRODUCT_CREATE = "ENDPOINT:ApiProductCreate";

    public enum CODE_ERROR
    {
        GATEWAY = -999999,
        IMPLEMEN = -888888,
    }
}
