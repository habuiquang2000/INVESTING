namespace BaseLib.Dtos;

public abstract class BaseEDto<T>
{
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
    public T? Data { get; set; }

    public const string RESPONSE_DATA_NULL = null;
    public const string RESPONSE_CONTENT_TYPE_JSON = "application/json";
}
public abstract class BaseEDto : BaseEDto<object>
{
}
