using BaseLib.Models;
using Gateway.BLL;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Gateway.Helper;

public static class BaseAuthentication
{
    /// <summary>
    /// Another code
    /// https://www.youtube.com/watch?v=3Q_aNm6gJiM
    /// </summary>
    public static async Task<string> GenerateToken(
        IConfiguration _configuration,
        string id,
        string username
    )
    {
        string jwtSecret = _configuration[CConfigAG.JWT_SECRET];
        string issuer = _configuration[CConfigAG.JWT_VALID_ISSUER];
        string audience = _configuration[CConfigAG.JWT_VALID_AUDIENCE];
        string expireType = _configuration[CConfigAG.JWT_EXPIRE_TYPE];
        int expireTime = int.TryParse(
            _configuration[CConfigAG.JWT_EXPIRE_DATETIME],
            out _)
            ? int.Parse(_configuration[CConfigAG.JWT_EXPIRE_DATETIME])
            : 0;

        List<Claim> jwtClaims = new();
        jwtClaims.AddRange(new List<Claim>
            {
                new(
                    JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString().ToLower()
                ),
                new(JwtClaim.Id, id),
                new(JwtClaim.Username, username),
            });


        SymmetricSecurityKey authSigningKey = new(Encoding.UTF8.GetBytes(jwtSecret));

        DateTime now = DateTime.Now;
        switch (expireType)
        {
            case "d":
                now.AddDays(expireTime); // Các bạn có thể set thời gian hết hạn của token ở đây
                break;
            case "h":
                now.AddHours(expireTime);
                break;
            case "m":
                now.AddMinutes(expireTime);
                break;
            case "s":
                now.AddSeconds(expireTime);
                break;

        }

        JwtSecurityToken token = new(
            issuer,
            audience,
            expires: now,
            claims: jwtClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256
            )
        );

        return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
    }

    public static JwtSecurityToken DecodeJwt(string jwt)
    {
        JwtSecurityTokenHandler handler = new();
        return handler.ReadJwtToken(jwt);
    }
    public static UserModel GetClaims(string jwt)
    {
        JwtSecurityToken? jwtSecurityToken = DecodeJwt(jwt);
        IEnumerable<Claim> claims = jwtSecurityToken.Claims;
        return new()
        {
            Id = claims.FirstOrDefault(o => o.Type == JwtClaim.Id)!.Value,
            Username = claims.FirstOrDefault(o => o.Type == JwtClaim.Username)!.Value
        };
    }
    public static DateTime GetValidTo(string jwt)
    {
        JwtSecurityToken? jwtSecurityToken = DecodeJwt(jwt);
        return jwtSecurityToken.ValidTo;
    }
}

//public class BaseAuthentication : AuthenticationHandler<AuthenticationSchemeOptions>
//{
//    public BaseAuthentication(
//    IOptionsMonitor<AuthenticationSchemeOptions> options,
//    ILoggerFactory logger,
//    UrlEncoder encoder,
//    ISystemClock clock
//    ) : base(options, logger, encoder, clock){}

//    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
//    {
//        var authHeader = Request.Headers["Authorization"].ToString();
//        if (authHeader != null && authHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
//        {
//            var token = authHeader.Substring("Basic ".Length).Trim();
//            System.Console.WriteLine(token);
//            var credentialstring = Encoding.UTF8.GetString(Convert.FromBase64String(token));
//            var credentials = credentialstring.Split(':');
//            if (credentials[0] == "admin" && credentials[1] == "admin")
//            {
//                var claims = new[] { new Claim("name", credentials[0]), new Claim(ClaimTypes.Role, "Admin") };
//                var identity = new ClaimsIdentity(claims, "Basic");
//                var claimsPrincipal = new ClaimsPrincipal(identity);
//                return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
//            }

//            Response.StatusCode = 401;
//            Response.Headers.Add("WWW-Authenticate", "Basic realm=\"dotnetthoughts.net\"");
//            return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
//        }
//        else
//        {
//            Response.StatusCode = 401;
//            Response.Headers.Add("WWW-Authenticate", "Basic realm=\"dotnetthoughts.net\"");
//            return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
//        }
//    }
//    //private readonly IUserBaseAuthen _userRepository;
//    //public BaseAuthentication(
//    //    IOptionsMonitor options,
//    //    ILoggerFactory logger,
//    //    UrlEncoder encoder,
//    //    ISystemClock clock,
//    //    IUserBaseAuthen userRepository
//    //) : base(options, logger, encoder, clock)
//    //{
//    //    _userRepository = userRepository;
//    //}

//    //protected async override Task HandleAuthenticateAsync()
//    //{
//    //    var authorizationHeader = Request.Headers["Authorization"].ToString();
//    //    if (authorizationHeader != null && authorizationHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
//    //    {
//    //        var token = authorizationHeader.Substring("Basic ".Length).Trim();
//    //        var credentialsAsEncodedString = Encoding.UTF8.GetString(Convert.FromBase64String(token));
//    //        var credentials = credentialsAsEncodedString.Split(':');
//    //        if (await _userRepository.Authenticate(credentials[0], credentials[1]))
//    //        {
//    //            var claims = new[] { new Claim("name", credentials[0]), new Claim(ClaimTypes.Role, "Admin") };
//    //            var identity = new ClaimsIdentity(claims, "Basic");
//    //            var claimsPrincipal = new ClaimsPrincipal(identity);
//    //            return await Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
//    //        }
//    //    }
//    //    Response.StatusCode = 401;
//    //    Response.Headers.Add("WWW-Authenticate", "Basic realm=\"joydipkanjilal.com\"");
//    //    return await Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
//    //}
//}
