using BaseLib.Dtos.User;
using BaseLib.Models;
using SqlExecute.ExecuteQueries;
using System.Data;
using System.Data.SqlClient;

namespace DataService.ExecuteQueries;

public class UserQuery : BaseQuery
{
    #region C
    public static string? InsertOne(UserModel user)
        => UpdateScala("USER_C_REGISTER", new List<SqlParameter>()
        {
            new SqlParameter("@_id", user.Id),
            new SqlParameter("@username", user.Username),
            new SqlParameter("@password", user.Password),
            new SqlParameter("@auth_pass", user.AuthPass),
        });
    #endregion

    #region R
    public static string SelectStatusByUsername(UserLoginDto login)
        => SelectValue("USER_R_STATUS_BY_USERNAME", new List<SqlParameter>()
        {
             new SqlParameter("@username", login.Username),
        });
    public static DataTable SelectByUsernameAndPassword(UserLoginDto login)
        => SelectByCondition("USER_R_BY_USERNAME_PASSWORD", new List<SqlParameter>()
        {
            new SqlParameter("@username", login.Username),
            new SqlParameter("@password", login.Password),
        });
    #endregion

    #region U
    #endregion

    #region D
    #endregion
}
