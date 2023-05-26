using Gateway.Implementations;
using Gateway.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Gateway.Helpers;

public static class TokenHelper
{
    public static string GenerateToken(
        IConfiguration _configuration,
        string id,
        string userName
    )
    {
        string jwtSecret = _configuration["JWT:Secret"];
        string issuer = _configuration["JWT:ValidIssuer"];
        string audience = _configuration["JWT:ValidAudience"];
        string expireType = _configuration["JWT:ExpireType"];
        int expireTime = int.TryParse(
            _configuration["JWT:ExpireDateTime"],
            out _)
            ? int.Parse(_configuration["JWT:ExpireDateTime"])
            : 0;

        List<Claim> jwtClaims = new();
        jwtClaims.AddRange(new List<Claim>
            {
                new(
                    JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString().ToLower()
                ),
                new(JwtClaimType.Id, id.ToLower()),
                new(JwtClaimType.Username, userName),
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

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static DateTime GetValidTo(string jwt)
    {
        JwtSecurityTokenHandler handler = new();
        JwtSecurityToken? jwtSecurityToken = handler.ReadJwtToken(jwt);
        return jwtSecurityToken.ValidTo;
    }
}