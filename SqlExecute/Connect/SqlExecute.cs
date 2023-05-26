using System.Data;
using System.Data.SqlClient;

namespace SqlExecute.Connect;

public class SqlConnect
{
    public static string? ConnectionString { get; set; }
    static SqlConnection? ConnectSql;

    #region Check Connect And Connect
    public static void Open(string? connectString = null)
    {
        try
        {
            Close();
            if (ConnectSql == null || ConnectSql.State == ConnectionState.Closed)
            {
                ConnectSql = new SqlConnection(connectString ?? ConnectionString);
                ConnectSql.Open();
            }
        }
        catch (SqlException error)
        {
            throw new Exception(error.Message);
        }
    }
    public static void Close()
    {
        if (ConnectSql != null && ConnectSql.State == ConnectionState.Open)
            ConnectSql.Close();
    }
    #endregion

    #region Commands
    private static readonly CommandType commandType = CommandType.StoredProcedure;
    public static SqlCommand Command(
        string? procedure,
        List<SqlParameter>? paramLists = null
    //CommandType commandType = default
    )
    {
        Open();
        SqlCommand sqlCommand = new(procedure, ConnectSql)
        {
            CommandType = (commandType == default) ? commandType : commandType,
        };

        if (paramLists != null)
            sqlCommand.Parameters.AddRange(paramLists.ToArray());

        return sqlCommand;
    }
    public static SqlCommand Command(
        string? procedure,
        DataTable? dt = null
    //CommandType commandType = default
    )
    {
        Open();
        SqlCommand sqlCommand = new(procedure, ConnectSql)
        {
            CommandType = (commandType == default) ? commandType : commandType,
        };

        sqlCommand.Parameters.AddWithValue("@table", dt);

        return sqlCommand;
    }
    #endregion
}

#region Execute Commands
public class ExecuteCommand : SqlConnect
{
    public static int ExecuteNonQuery(
        string? procedure = null,
        List<SqlParameter>? paramLists = null
    )
    {
        if (string.IsNullOrEmpty(procedure)) return 0;

        int i = Command(procedure, paramLists)
            .ExecuteNonQuery();
        Close();
        return i;
    }
    public static int ExecuteNonQuery(string? procedure = null, DataTable? dt = null)
    {
        if (string.IsNullOrEmpty(procedure)) return 0;

        int i = Command(procedure, dt)
            .ExecuteNonQuery();
        Close();
        return i;
    }
    public static string ExecuteScala(
        string? procedure = null,
        List<SqlParameter>? paramLists = null
    )
    {
        if (string.IsNullOrEmpty(procedure))
        {
            return "";
        }

        object execute = Command(procedure, paramLists)
            .ExecuteScalar();
        string i = $"{execute}"
            .ToString();

        Close();
        return i;
    }
    public static DataTable FillDt(string? procedure = null, List<SqlParameter>? paramLists = null)
    {
        if (string.IsNullOrEmpty(procedure)) return new();

        SqlDataAdapter da = new(Command(procedure, paramLists));
        DataTable dt = new();
        da.Fill(dt);
        Close();
        return dt;
    }
    public static DataSet FillDs(string? procedure = null, List<SqlParameter>? paramLists = null)
    {
        if (string.IsNullOrEmpty(procedure)) return new();

        SqlDataAdapter da = new(Command(procedure, paramLists));
        DataSet ds = new();
        da.Fill(ds);
        Close();
        return ds;
    }
}
#endregion
