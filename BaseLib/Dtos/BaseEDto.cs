namespace BaseLib.Dtos;

public enum ResponseMessage
{
    SUCCESS,
    FAILURE,
    PENDDING,
}

public abstract class BaseResponseDto
{
    public int Code { get; set; }
    public string Message { get; set; }

    public BaseResponseDto(int Code, string Message)
    {
        this.Code = Code;
        this.Message = Message;
    }
}
