namespace Gateway.BLL;

public class CConfigAG
{
    public static readonly string HOST_PRODUCT = "DataServiceHosts:DSProductApiService";
    public const string ENDPOINT_PRODUCT_LIST = "ENDPOINT:ApiProductList";
    public const string ENDPOINT_PRODUCT_CREATE = "ENDPOINT:ApiProductCreate";

    public const string ENDPOINT_CHECK_USERNAME = "ENDPOINT:ApiCheckUsername";
    public const string ENDPOINT_LOGIN = "ENDPOINT:ApiLogin";
    public const string ENDPOINT_REGISTER = "ENDPOINT:ApiRegister";

    public const string JWT_SECRET = "JWT:Secret";
    public const string JWT_VALID_ISSUER = "JWT:ValidIssuer";
    public const string JWT_VALID_AUDIENCE = "JWT:ValidAudience";
    public const string JWT_EXPIRE_TYPE = "JWT:ExpireType";
    public const string JWT_EXPIRE_DATETIME = "JWT:ExpireDateTime";

    public enum CODE_ERROR
    {
        GATEWAY = -999999,
        IMPLEMEN = -888888,
    }
}
