using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BaseLib.Controllers;

/// base controller de kiem tra authenticate cho tat ca request
/// https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters?view=aspnetcore-3.1
public class ApiBaseController : ControllerBase
{
    public IActionResult CreateJsonResponse(object data)
    {
        try
        {
            string json = JsonConvert.SerializeObject(data);

            return Content(json); // OK
        }
        catch
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
