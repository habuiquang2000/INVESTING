using BaseLib.Models;
using SqlExecute.ExecuteQueries;
using System.Data;
using System.Data.SqlClient;

namespace DataService.ExecuteQueries;

public class ProductQuery : BaseQuery
{
    #region C
    public static DataTable InsertOne(ProductModel product)
        => UpdateAndGetNew("PRODUCT_C_ONE", new List<SqlParameter>()
        {
            new SqlParameter("@_id", product.Id),
            new SqlParameter("@name", product.Name),
            new SqlParameter("@price", product.Price),
            new SqlParameter("@description", product.Description),
        });
    #endregion

    #region R
    public static DataTable GetAll(string search = "")
        => SelectByCondition("PRODUCT_R_MANY", new List<SqlParameter>()
        {
            new SqlParameter("@search", search),
        });
    #endregion

    #region U
    #endregion

    #region D
    public static string? DeleteOne(ProductModel product)
        => UpdateScala("PRODUCT_D_ONE", new List<SqlParameter>()
        {
             new SqlParameter("@id", product.Id),
        });
    #endregion
}
