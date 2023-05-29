namespace Gateway.BLL;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        var token = context
            .Request
            .Headers["Authorization"]
            .FirstOrDefault()?
            .Split(" ").Last();
        //if (token != null)
            //Validate the token
            //attachUserToContext(context, userService, token);
        await _next(context);
    }
}
