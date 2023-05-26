namespace BaseLib.Dtos;

public class EDalResult
{
    public enum CODE
    {
        ERROR = -2, // co loi
        INIT = -1, // khoi tao
        SUCCESS = 0,    // thanh cong
    }

    public enum STRING
    {
        INIT, // moi khoi tao object xong
        ERROR, // co loi xay ra trong app code
        SUCCESS, // thanh cong
    }

    /// <summary>
    /// code return tu sp hoac system neu co exception
    /// </summary>
    public long Code { get; set; }

    /// <summary>
    /// message return tu sp hoac error msg neu co exception
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// data khong xac dinh type
    /// </summary>
    public object? Data { get; set; }

    /// <summary>
    /// constructor
    /// </summary>
    public EDalResult() { Code = (long)CODE.INIT; Message = STRING.INIT.ToString(); Data = null; }
}
