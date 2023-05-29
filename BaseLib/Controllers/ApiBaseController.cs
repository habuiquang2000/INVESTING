using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BaseLib.Controllers;

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
