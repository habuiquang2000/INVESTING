using BaseLib.Dtos.User;
using BaseLib.Models;
using DataService.ExecuteQueries;
using DataService.Utils;
using System.Data;
using static BaseLib.Enums.UserEnum;

namespace DataService.Services
{
    public class UserService : BaseService
    {
        #region C
        public static UserModel? Register(UserRegisterDto user)
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
                UserQuery.InsertOne(newUser);
                return new()
                {
                    Id = Uuid.GenerateWithDateTime,
                    Username = user.Username,
                };
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region R
        public static LOGIN_STATE CheckUsername(
            UserLoginDto login
        )
        {
            string status = UserQuery.SelectStatusByUsername(login);
            LOGIN_STATE loginStatus = (LOGIN_STATE)Enum.Parse(
                typeof(LOGIN_STATE),
                status
            );
            return loginStatus;
        }
        public static UserModel? CheckLogin(UserLoginDto login)
        {
            DataTable userLogin = UserQuery.SelectByUsernameAndPassword(new UserLoginDto()
            {
                Username = login.Username,
                Password = Encrypt.GenSaltMD5(login.Password ?? "")
            });

            foreach (DataRow dr in userLogin.Rows)
            {
                return new UserModel()
                {
                    Id = $"{dr["_id"]}",
                    Username = $"{dr["username"]}",
                };
            }
            return null;
        }
        #endregion

        #region U
        #endregion

        #region D
        #endregion
    }
}
