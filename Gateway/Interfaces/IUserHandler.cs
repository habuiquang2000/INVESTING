using BaseLib.Dtos;
using BaseLib.Dtos.User;

namespace Gateway.Interfaces;

public interface IUserHandler
{
    Task<EResponse> Api_CheckLoginUserAsync(string? accessToken);
    Task<EResponse> Api_LoginUserAsync(UserLoginDto? TUM);
    Task<EResponse> Api_RegisterUserAsync(UserRegisterDto? TUM);
}
