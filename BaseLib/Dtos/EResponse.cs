namespace BaseLib.Dtos;

public class EResponse : BaseEDto
{
    public enum RESPONSE_CODE
    {
        INIT = -1,
        INVALID_TRADING_PASSWORD = -10,
        ACCESS_DENIED = -123456,
        SUCCESS = 0,
        INVALID_DATA = -20,
    }
}
