using BaseLib.Models;
using SqlExecute.ExecuteQueries;
using System.Data;
using System.Data.SqlClient;

namespace DataService.ExecuteQueries;

public class ProductQuery : BaseQuery
{
    #region C
    public static async Task<DataTable> InsertOneAsync(ProductModel product)
        => await Task.FromResult(UpdateAndGetNew("PRODUCT_C_ONE", new List<SqlParameter>()
        {
            new SqlParameter("@_id", product.Id),
            new SqlParameter("@name", product.Name),
            new SqlParameter("@price", product.Price),
            new SqlParameter("@description", product.Description),
        }));
    #endregion

    #region R
    public static async Task<DataTable> GetAllAsync(string search = "")
        => await Task.FromResult(SelectByCondition("PRODUCT_R_MANY", new List<SqlParameter>()
        {
            new SqlParameter("@search", search),
        }));
    #endregion

    #region U
    #endregion

    #region D
    #endregion
}
