using BaseLib.Controllers;
using BaseLib.Dtos;
using BaseLib.Dtos.User;
using Gateway.BLL;
using Gateway.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Gateway.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ApiBaseController
{
    private readonly IUserHandler _httpClientHandler;

    public UserController(
        IUserHandler httpClientHandler
    )
    {
        _httpClientHandler = httpClientHandler;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> PRegister(
        [FromBody] UserRegisterDto? register
    )
    {
        try
        {
            EResponse responseResult = await _httpClientHandler.Api_RegisterUserAsync(register);

            return Content(JsonConvert.SerializeObject(responseResult));
        }
        catch (Exception ex)
        {
            return CreateJsonResponse(new EResponse()
            {
                Code = (long)CConfigAG.CODE_ERROR.GATEWAY,
                Message = ex.Message,
                Data = EResponse.RESPONSE_DATA_NULL
            });
        }
    }

    [HttpPost("Login")]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> PLogin(
        [FromBody] UserLoginDto? login
    )
    {
        try
        {
            EResponse responseResult = await _httpClientHandler
                .Api_LoginUserAsync(login);

            return Content(JsonConvert.SerializeObject(responseResult));
        }
        catch (Exception ex)
        {
            return CreateJsonResponse(new EResponse()
            {
                Code = (long)CConfigAG.CODE_ERROR.GATEWAY,
                Message = ex.Message,
                Data = EResponse.RESPONSE_DATA_NULL
            });
        }
    }

    [HttpGet("CheckLogin")]
    [Authorize]
    //[Authorize(Roles = SecurityRoles.Admin)]
    public async Task<IActionResult> GCheckLogin()
    {
        //string token = Request
        //    .Headers[HeaderNames.Authorization]
        //    .ToString()
        //    .Replace("Bearer ", "");
        string? accessToken = await HttpContext
            .GetTokenAsync("access_token");
        try
        {
            EResponse responseResult = await _httpClientHandler
                .Api_CheckLoginUserAsync(accessToken);

            return Content(JsonConvert.SerializeObject(responseResult));
        }
        catch (Exception ex)
        {
            return CreateJsonResponse(new EResponse()
            {
                Code = (long)CConfigAG.CODE_ERROR.GATEWAY,
                Message = ex.Message,
                Data = EResponse.RESPONSE_DATA_NULL
            });
        }
    }
}
