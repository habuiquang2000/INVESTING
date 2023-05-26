using SqlExecute.Connect;
using System.Data;
using System.Data.SqlClient;

namespace SqlExecute.ExecuteQueries;

public abstract class BaseQuery
{
    #region R
    public static DataTable SelectAll(string procedure)
        => ExecuteCommand.FillDt(procedure);
    public static DataTable SelectByCondition(string procedure, List<SqlParameter> paramLists)
        => ExecuteCommand.FillDt(procedure, paramLists);
    public static string SelectValue(string procedure)
        => ExecuteCommand.ExecuteScala(procedure);
    public static string SelectValue(string procedure, List<SqlParameter> paramLists)
        => ExecuteCommand.ExecuteScala(procedure, paramLists);
    #endregion

    #region C, U, D
    public static int Update(string procedure, List<SqlParameter> paramLists)
        => ExecuteCommand.ExecuteNonQuery(procedure, paramLists);
    public static DataTable UpdateAndGetNew(string procedure, List<SqlParameter> paramLists)
        => ExecuteCommand.FillDt(procedure, paramLists);
    public static int Update(string procedure, DataTable dt)
        => ExecuteCommand.ExecuteNonQuery(procedure, dt);
    public static string UpdateScala(string procedure, List<SqlParameter> paramLists)
        => ExecuteCommand.ExecuteScala(procedure, paramLists);
    #endregion
}
