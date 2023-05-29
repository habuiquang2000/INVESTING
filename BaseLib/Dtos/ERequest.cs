namespace BaseLib.Dtos;

public class ERequest<T> : BaseEDto<T>
{
    public enum CODE
    {
        INIT = -1, // Khoi tao object xong
        ERROR = -2, // Co loi xay ra trong app code
        SUCCESS = 0, // Thanh cong
    }
    public enum LOGIN_CODE
    {
        NOT_EXIST = 0, // Co loi xay ra trong app code
        SUCCESS = 1, // Thanh cong
    }
}
public class ERequest : ERequest<object>
{
    ///<summary>
    ///constructor
    ///</summary>
    public ERequest()
    {
        Code = (long)CODE.INIT;
        Message = CODE.INIT.ToString();
        Data = null;
    }
}
