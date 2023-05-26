namespace BaseLib.Dtos;

public class EResponseResult
{
    public const string RESPONSE_DATA_NULL = null;
    public const string RESPONSE_CONTENT_TYPE_JSON = "application/json";

    public enum RESPONSE_MSG
    {
        SUCCESS,
        INIT,
        ACCESS_DENIED,
    }

    public enum RESPONSE_CODE
    {
        INIT = -1,
        INVALID_TRADING_PASSWORD = -10,
        ACCESS_DENIED = -123456,
        SUCCESS = 0,
        INVALID_DATA = -20,
    }

    /// <summary>
    /// code return tu sp hoac system neu co exception
    /// </summary>
    public long Code { get; set; }

    /// <summary>
    /// message return tu sp hoac error msg neu co exception
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// data khong xac dinh type
    /// </summary>
    public object? Data { get; set; }
}
