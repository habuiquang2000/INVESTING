using BaseLib.Controllers;
using Gateway.Helpers;
using Gateway.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly IConfiguration _configuration;
    //private readonly IGatewayHandler _handler;

    public UserController(
        IConfiguration configuration
        //, IGatewayHandler handler
    )
    {
        _configuration = configuration;
        //_handler = handler;
    }

    [HttpPost("Login")]
    //[ValidateAntiForgeryToken]
    //public Task<IActionResult> PLogin([FromBody] LoginDto login)
    public IActionResult PLogin()
    {
        //UserService.Login(login);

        var token = TokenHelper.GenerateToken(
            _configuration,
            "",
            ""
        );

        return Ok(new
        {
            Token = token,
            ValidTo = TokenHelper.GetValidTo(token)
        });
    }

    //[HttpPost("Register")]
    //public IEnumerable<int> PRegister()
    //{

    //}


    [HttpGet("CheckLogin")]
    [Authorize]
    //[Authorize(Roles = SecurityRoles.Admin)]
    public ActionResult<int> GCheckLogin()
    {
        return Ok(new
        {
            Message = "Chưa đăng nhập"
        });
    }
    //    [HttpPost]
    //    public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
    //    {
    //        var existUsername = await _userManager.FindByNameAsync(dto.UserName);

    //        if (existUsername != null) return new BadRequestObjectResult($"Username {dto.UserName} has already been taken");

    //        var appUser = UserHelper.ToApplicationUser(dto);

    //        var result1 = await _userManager.CreateAsync(appUser, dto.Password);

    //        if (!result1.Succeeded) return new BadRequestObjectResult(result1.Errors);

    //        List<string> roles = new();

    //        if (dto.IsAdmin) roles.AddRange(SecurityRoles.Roles.ToList());
    //        else roles.Add("User");

    //        var result2 = await _userManager.AddToRolesAsync(appUser, roles);

    //        if (!result2.Succeeded) return new BadRequestObjectResult(result2.Errors);

    //        var response = await _userManager.FindByNameAsync(dto.UserName);

    //        return Ok(response);
    //    }
}
