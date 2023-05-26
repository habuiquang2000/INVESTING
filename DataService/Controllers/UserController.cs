using BaseLib.Dtos.User;
using BaseLib.Models;
using DataService.Services;
using Microsoft.AspNetCore.Mvc;
using static BaseLib.Enums.UserEnum;

namespace DataService.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class UserController : ControllerBase
{
    public UserController(){ }

    [HttpPost]
    public async Task<ActionResult<LOGIN_STATE>> PCheckUsername(
        [FromBody] UserLoginDto userLogin
    )
    {
        LOGIN_STATE loginStatus = UserService.CheckUsername(userLogin);
        return await Task.FromResult(Ok(loginStatus));
    }

    [HttpPost]
    public ActionResult<UserModel> PCheckLogin(
        [FromBody] UserLoginDto userLogin
    )
    {
        UserModel? user = UserService.CheckLogin(userLogin);
        return Ok(user);
    }

    [HttpPost]
    public ActionResult<UserModel> PRegister(
        [FromBody] UserRegisterDto userRegister
    )
    {
        UserModel? user = UserService.Register(userRegister);
        return Ok(user);
    }
}
