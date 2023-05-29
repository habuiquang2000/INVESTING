using BaseLib.Controllers;
using BaseLib.Dtos;
using BaseLib.Dtos.User;
using DataService.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DataService.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class UserController : ApiBaseController
{
    public UserController(){ }

    [HttpPost]
    public async Task<IActionResult> PCheckUsername(
        [FromBody] UserLoginDto userLogin
    )
    {
        try
        {
            ERequest responseResult = await UserService.CheckUsername(userLogin);

            return Content(JsonConvert.SerializeObject(responseResult));
        }
        catch (Exception ex)
        {
            return CreateJsonResponse(new ERequest()
            {
                Code = (long)ERequest.CODE.ERROR,
                Message = ex.Message,
                Data = null
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> PCheckLogin(
        [FromBody] UserLoginDto userLogin
    )
    {
        try
        {
            ERequest responseResult = await UserService.CheckLogin(userLogin);

            return Content(JsonConvert.SerializeObject(responseResult));
        }
        catch (Exception ex)
        {
            return CreateJsonResponse(new ERequest()
            {
                Code = (long)ERequest.CODE.ERROR,
                Message = ex.Message,
                Data = null
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> PRegister(
        [FromBody] UserRegisterDto userRegister
    )
    {
        try
        {
            ERequest responseResult = await UserService.Register(userRegister);

            return Content(JsonConvert.SerializeObject(responseResult));
        }
        catch (Exception ex)
        {
            return CreateJsonResponse(new ERequest()
            {
                Code = (long)ERequest.CODE.ERROR,
                Message = ex.Message,
                Data = null
            });
        }
    }
}
