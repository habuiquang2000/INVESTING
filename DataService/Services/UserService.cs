using BaseLib.Dtos;
using BaseLib.Dtos.User;
using BaseLib.Models;
using DataService.ExecuteQueries;
using DataService.Utils;
using System.Data;

namespace DataService.Services;

public class UserService
{
    static readonly Dictionary<string, string> userMapper = new()
    {
        ["Id"] = "_id",
        ["Username"] = "username",
    };
    #region C
    public static async Task<ERequest> Register(UserRegisterDto user)
    {
        try
        {
            UserModel newUser = new()
            {
                Id = Uuid.GenerateWithDateTime,
                Username = user.Username,
                Password = Encrypt.GenSaltMD5(user.Password ?? ""),
                AuthPass = user.AuthPass,
            };

            await UserQuery.InsertOne(newUser);

            return new ERequest()
            {
                Code = (long)ERequest.CODE.SUCCESS,
                Message = ERequest.CODE.SUCCESS.ToString(),
                Data = new
                {
                    newUser.Id,
                    newUser.Username,

                },
            };
        }
        catch (Exception ex)
        {
            return new ERequest()
            {
                Code = ((long)ERequest.CODE.ERROR),
                Message = ex.Message,
                Data = ERequest.RESPONSE_DATA_NULL
            };
        }
    }
    #endregion

    #region R
    public static async Task<ERequest> CheckUsername(
        UserLoginDto login
    )
    {
        try
        {
            string status = await UserQuery.SelectStatusByUsername(login);
            ERequest.LOGIN_CODE loginStatus = (ERequest.LOGIN_CODE)Enum.Parse(
                typeof(ERequest.LOGIN_CODE),
                status
            );

            return new ERequest()
            {
                Code = (long)ERequest.CODE.SUCCESS,
                Message = ERequest.CODE.SUCCESS.ToString(),
                Data = loginStatus,
            };
        }
        catch (Exception ex)
        {
            return new ERequest()
            {
                Code = ((long)ERequest.CODE.ERROR),
                Message = ex.Message,
                Data = ERequest.RESPONSE_DATA_NULL
            };
        }
    }
    public static async Task<ERequest> CheckLogin(UserLoginDto login)
    {
        try
        {
            DataTable userDt = await UserQuery
                .SelectByUsernameAndPassword(
                new UserLoginDto()
                {
                    Username = login.Username,
                    Password = Encrypt.GenSaltMD5(login.Password ?? "")
                });
            List<UserModel>? userLst = userDt.ToList<UserModel>(userMapper);

            if(userLst == null || userLst.Count == 0)
            {
                throw new BadHttpRequestException(EResponse.RESPONSE_CODE.ACCESS_DENIED.ToString());
            }

            UserModel user = userLst.First();

            return new ERequest()
            {
                Code = (long)ERequest.CODE.SUCCESS,
                Message = ERequest.CODE.SUCCESS.ToString(),
                Data = new {
                    user.Id,
                    user.Username,
                },
            };
        }
        catch (Exception ex)
        {
            return new ERequest()
            {
                Code = ((long)ERequest.CODE.ERROR),
                Message = ex.Message,
                Data = ERequest.RESPONSE_DATA_NULL
            };
        }
    }
    #endregion

    #region U
    #endregion

    #region D
    #endregion
}
